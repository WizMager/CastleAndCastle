using System;
using System.IO;
using UnityEngine;

namespace Ecs.Extensions.UidGenerator
{
    public static unsafe class UidGenerator
    {
        private const string FILE_NAME = "uid";

        private static readonly object Locker = new();

        private static string _uidFilePath;
        private static byte[] _buffer;
        private static uint _current;

        static UidGenerator()
        {
            if (!Application.isEditor)
                _buffer = Initialize();
        }

        private static byte[] Initialize()
        {
            _uidFilePath = Path.Combine(Application.persistentDataPath, FILE_NAME);
            if (!File.Exists(_uidFilePath))
                return new byte[sizeof(uint)];

            var bytes = File.ReadAllBytes(_uidFilePath);

            if (bytes.Length != sizeof(uint))
            {
                Debug.LogException(new Exception($"[{nameof(UidGenerator)}] Uid data corrupted"));
            }

            fixed (byte* ptr = &bytes[0])
            {
                _current = *(uint*)ptr;
            }

            return bytes;
        }

        public static Uid Next()
        {
            lock (Locker)
            {
#if UNITY_EDITOR
                _buffer ??= Initialize();
#endif

                if (_current == uint.MaxValue)
                    throw new Exception($"[{nameof(UidGenerator)}] Uid reached max value: {uint.MaxValue}");

                _current++;

                fixed (byte* ptr = &_buffer[0])
                {
                    *(uint*)ptr = _current;
                }

                File.WriteAllBytes(_uidFilePath, _buffer);

                return (Uid)_current;
            }
        }
    }
}