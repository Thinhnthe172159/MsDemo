using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Demo.Helpers
{
    public static class MisaEntityHelper
    {
        // ================= CACHE =================
        private static readonly ConcurrentDictionary<Type, Metadata> _cache = new();

        // ================= PUBLIC API =================

        public static string GetTableName<T>()
            => GetMetadata(typeof(T)).TableName;

        public static string GetKeyColumn<T>()
            => GetMetadata(typeof(T)).KeyColumnName;

        public static string GetKeyProperty<T>()
            => GetMetadata(typeof(T)).KeyProperty.Name;

        public static List<ColumnValue> GetColumnValues<T>(
            T entity,
            bool includeKey = false,
            bool ignoreNull = true
        )
        {
            var meta = GetMetadata(typeof(T));
            var result = new List<ColumnValue>();

            foreach (var prop in meta.Properties)
            {
                if (!includeKey && prop == meta.KeyProperty)
                    continue;

                var value = prop.GetValue(entity);

                if (ignoreNull && value == null)
                    continue;

                var columnAttr = prop.GetCustomAttribute<ColumnAttribute>();
                var columnName = columnAttr?.Name ?? ToSnakeCase(prop.Name);

                result.Add(new ColumnValue
                {
                    ColumnName = columnName,
                    PropertyName = prop.Name,
                    Value = value
                });
            }

            return result;
        }

        // ================= CORE =================

        private static Metadata GetMetadata(Type type)
        {
            return _cache.GetOrAdd(type, BuildMetadata);
        }

        private static Metadata BuildMetadata(Type type)
        {
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            if (tableAttr == null)
                throw new Exception($"{type.Name} thiếu [Table]");

            var properties = type.GetProperties(
                BindingFlags.Public | BindingFlags.Instance
            );

            var keyProp = properties.FirstOrDefault(
                p => p.GetCustomAttribute<KeyAttribute>() != null
            );

            if (keyProp == null)
                throw new Exception($"{type.Name} thiếu [Key]");

            var columnAttr = keyProp.GetCustomAttribute<ColumnAttribute>();
            var keyColumn = columnAttr?.Name ?? ToSnakeCase(keyProp.Name);

            return new Metadata
            {
                TableName = tableAttr.Name,
                KeyProperty = keyProp,
                KeyColumnName = keyColumn,
                Properties = properties
            };
        }

        // ================= NAMING =================

        private static string ToSnakeCase(string name)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < name.Length; i++)
            {
                var c = name[i];

                if (char.IsUpper(c))
                {
                    if (i > 0)
                        sb.Append('_');

                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        // ================= INNER MODELS =================

        private class Metadata
        {
            public string TableName { get; set; }
            public PropertyInfo KeyProperty { get; set; }
            public string KeyColumnName { get; set; }
            public PropertyInfo[] Properties { get; set; }
        }

        public class ColumnValue
        {
            public string ColumnName { get; set; }
            public string PropertyName { get; set; }
            public object Value { get; set; }
        }
    }
}
