using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MehodArchive
{
    public static class TypeFinderExpansionMethods
    {
        public static IEnumerable<Type> GetTypeBy(params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(assembly =>
                {
                    try
                    {
                        return assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException reflTypeLoadEx)
                    {
                        return reflTypeLoadEx.Types;
                    }
                })
                .Where(type => type is not null)!;

            return types;
        }
        #region Async
        public static async Task<IEnumerable<Type>> AsyncGetTypeBy(params Assembly[] assemblies)
        {
            IEnumerable<Type> types = await Task.Run<IEnumerable<Type>>(() => assemblies.SelectMany(assembly =>
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException reflTypeLoadEx)
                {
                    return reflTypeLoadEx.Types;
                }
            })
                .Where(type => type is not null)!);

            return types;
        }
        #endregion

        public static IEnumerable<Type> GetAllType()
            => GetTypeBy(AppDomain.CurrentDomain.GetAssemblies());
        #region Async
        public static async Task<IEnumerable<Type>> AsyncGetAllType()
            => await AsyncGetTypeBy(AppDomain.CurrentDomain.GetAssemblies());
        #endregion

        public static IEnumerable<Type> GetAllTypeWhere(Predicate<Type> condition)
            => GetAllType().Where(type => condition.Invoke(type));
        #region Async
        public static async Task<IEnumerable<Type>> AsyncGetAllTypeWhere(Predicate<Type> condition)
            => (await AsyncGetAllType()).Where(type => condition.Invoke(type));
        #endregion

        public static IEnumerable<Type> GetAllClass()
            => GetAllTypeWhere(type => type.IsClass);
        #region Async
        public static async Task<IEnumerable<Type>> GetAllClassAsync()
           => await AsyncGetAllTypeWhere(type => type.IsClass);
        #endregion
    }
}
