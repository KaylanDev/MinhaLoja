using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MinhaLoja.Core.Dtos.Extensions
{
    public static class ExtensionsDto
    {
        public static ICollection<TDestination> FromEntities<TSource, TDestination>(
            this ICollection<TSource> entities)
        {
            if (entities == null) return null;

            var op = typeof(TDestination).GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(TSource) }, null);
            if (op == null)
                throw new InvalidOperationException($"Não existe operador implícito de {typeof(TSource).Name} para {typeof(TDestination).Name}.");

            return entities.Select(e => (TDestination)op.Invoke(null, new object[] { e })).ToList();
        }
    }
}
