using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace JSONTools
{
    public static class ExcelMatrix
    {
        /// <summary>
        /// Serializes a matrix of objects to JSON
        /// </summary>
        /// <param name="data">The matrix of objects to serialize.</param>
        /// <param name="handlers">Funcs describing how to handle specific keys.</param>
        /// <returns>The JSON from the serialized object matrix.</returns>
        public static string ToJson(IEnumerable<IEnumerable<object>> data,
            Dictionary<string, Func<object, object>> handlers = null)
        {
            return ToJson(
                data?.Select(l => l?.ToArray()).ToArray(),
                handlers);
        }

        /// <summary>
        /// Serializes a matrix of objects to JSON
        /// </summary>
        /// <param name="data">The matrix of objects to serialize.</param>
        /// <param name="handlers">Funcs describing how to handle specific keys.</param>
        /// <returns>The JSON from the serialized object matrix.</returns>
        public static string ToJson(
            IReadOnlyList<IReadOnlyList<object>> data,
            Dictionary<string, Func<object, object>> handlers = null)
        {
            if (data == null)
            {
                throw new ArgumentException("Argument data cannot be null");
            }

            var rowCount = data.Count;
            var colCount = data.Count > 0 ? data[0].Count : 0;

            var headers = data[0]
                .Select(v => v.ToString())
                .Where(h => !string.IsNullOrWhiteSpace(h))
                .Distinct()
                .ToArray();

            var dictionaries = new Dictionary<string, object>[rowCount - 1];
            for (var row = 1; row < rowCount; row++)
            {
                try
                {
                    dictionaries[row - 1] = new Dictionary<string, object>();
                    for (var col = 0; col < colCount; col++)
                    {
                        var key = headers[col];
                        object value;

                        if (col >= data[row].Count)
                        {
                            value = null;
                        }
                        else if (handlers != null && handlers.ContainsKey(key))
                        {
                            value = handlers[key](data[row][col]);
                        }
                        else
                        {
                            value = data[row][col];
                        }

                        dictionaries[row - 1].Add(key, value);
                    }
                }
                catch (Exception e)
                {
                    var foo = e;
                }
            }

            return JsonConvert.SerializeObject(dictionaries);
        }

        /// <summary>
        /// Deserializes the JSON to the specified .NET type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="data">The matrix of objects to deserialize.</param>
        /// <param name="handlers">Funcs describing how to handle specific keys.</param>
        /// <returns>The deserialized objects from the matrix of objects.</returns>
        public static IReadOnlyList<T> Deserialize<T>(
            IEnumerable<IEnumerable<object>> data,
            Dictionary<string, Func<object, object>> handlers = null)
        {
            var json = ToJson(data, handlers);
            return JsonConvert.DeserializeObject<T[]>(json);
        }
    }
}