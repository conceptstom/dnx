// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;

namespace Microsoft.Framework.Runtime.Json
{
    internal static class JsonDeserializerExtensions
    {
        public static JsonObject DeserializeAsObject(this JsonDeserializer deserializer, string input)
        {
            var dictionary = deserializer.Deserialize(input) as IDictionary<string, object>;

            if (dictionary != null)
            {
                return new JsonObject(dictionary);
            }
            else
            {
                return null;
            }
        }

        public static JsonObject DeserializeAsObject(this JsonDeserializer deserializer, Stream stream)
        {
            var reader = new StreamReader(stream);
            return deserializer.DeserializeAsObject(reader.ReadToEnd());
        }
    }
}
