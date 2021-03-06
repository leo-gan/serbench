﻿using System;
using System.IO;
using System.Linq;
using NFX;
using NFX.Environment;

using JsonFx.Json;

namespace Serbench.Specimens.Serializers
{
    /// <summary>
    ///     Represents JsonFx by Stephen McKamey:
    /// See here https://github.com/jsonfx/jsonfx
    /// >PM Install-Package JsonFx 
    /// </summary>
    public class JsonFxSerializer : Serializer
    {
        static readonly JsonWriter m_jw = new JsonWriter();
        static readonly JsonReader m_jr = new JsonReader();
        private Type m_primaryType;

        public JsonFxSerializer(TestingSystem context, IConfigSectionNode conf)
            : base(context, conf)
        {
        }

        public override void BeforeRuns(Test test)
        {
            m_primaryType = test.GetPayloadRootType();
        }

        public override void Serialize(object root, Stream stream)
        {
            using (var sw = new StreamWriter(stream))
            {
                sw.Write(m_jw.Write(root));
            }
        }

        public override object Deserialize(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return m_jr.Read(sr.ReadToEnd(), m_primaryType);
            }
        }

        public override void ParallelSerialize(object root, Stream stream)
        {
            using (var sw = new StreamWriter(stream))
            {
                sw.Write(m_jw.Write(root));
            }
        }

        public override object ParallelDeserialize(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return m_jr.Read(sr.ReadToEnd(), m_primaryType);
            }
        }
    }
}