using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MehodArchive
{
    public class MethodArchive
    {
        private Dictionary<string, Delegate> archiveMethodDictionary;

        public MethodArchive()
        {
            archiveMethodDictionary = new Dictionary<string, Delegate>();
        }

        public void Invoke(string name, params object[] parameters)
        {
            try
            {
                archiveMethodDictionary[name].DynamicInvoke(parameters);
            }
            catch (KeyNotFoundException ex)
            {
                throw new MethodArchiveException(ex, "Can't found method. check the you archived method.");
            }
            catch (TargetParameterCountException ex)
            {
                throw new MethodArchiveException(ex, "Parameter is out of range. check the parameter.");
            }
            catch (ArgumentException ex)
            {
                throw new MethodArchiveException(ex, "Type is not matched. check the parameter.");
            }
            catch (TargetInvocationException ex)
            {
                throw new MethodArchiveException(ex.InnerException, "Throwed exception in archived method.");
            }
        }

        public bool ArchiveMethod(string name, Delegate method)
            => archiveMethodDictionary.TryAdd(name, method);

        public bool ArchiveMethod(string name, MethodInfo methodInfo)
        {
            try
            {
                Delegate method = MethodArchiveHelper.MethodInfoCastToDelegate(methodInfo);
                return archiveMethodDictionary.TryAdd(name, method);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public bool ArchiveMethodsBy(Type type)
        {
            MethodArchiveHelper.ArchiveMethodInfo[]? archiveMethods = MethodArchiveHelper.GetArchiveStaticMehods(type)?.ToArray();

            for (int i = 0; i < (archiveMethods?.Length ?? 0); ++i)
            {
                MethodArchiveHelper.ArchiveMethodInfo methodArchiveAttribute = archiveMethods![i];
                Delegate method = MethodArchiveHelper.MethodInfoCastToDelegate(methodArchiveAttribute.method);
                ArchiveMethod(methodArchiveAttribute.name, method);
            }

            return archiveMethods is not null;
        }
        #region Async
        public async Task<bool> AsyncArchiveMethodsBy(Type type)
        {
            MethodArchiveHelper.ArchiveMethodInfo[]? archiveMethods = MethodArchiveHelper.GetArchiveStaticMehods(type)?.ToArray();

            for (int i = 0; i < (archiveMethods?.Length ?? 0); ++i)
            {
                await Task.Run(() =>
                {
                    MethodArchiveHelper.ArchiveMethodInfo methodArchiveAttribute = archiveMethods![i];
                    Delegate method = MethodArchiveHelper.MethodInfoCastToDelegate(methodArchiveAttribute.method);
                    ArchiveMethod(methodArchiveAttribute.name, method);
                });
            }

            return archiveMethods is not null;
        }
        #endregion

        public bool ArchiveMethodsBy(IEnumerable<Type> types)
        {
            if (0 >= (types?.Count() ?? 0))
                return false;

            foreach (Type type in types!)
                ArchiveMethodsBy(type);

            return true;
        }
        #region Async
        public async Task<bool> AsyncArchiveMethodsBy(IEnumerable<Type> types)
        {
            if (0 >= (types?.Count() ?? 0))
                return false;

            foreach (Type type in types!)
            {
                await AsyncArchiveMethodsBy(type);
            }

            return true;
        }
        #endregion

        public bool ArchiveAllMethod()
        {
            IEnumerable<Type> types = TypeFinderExpansionMethods.GetAllClass().Where(type => typeof(IArchivedMethods).IsAssignableFrom(type));

            return ArchiveMethodsBy(types);
        }
        #region Async
        public bool AsyncArchiveAllMethod()
        {
            IEnumerable<Type> types = TypeFinderExpansionMethods.GetAllClass().Where(type => typeof(IArchivedMethods).IsAssignableFrom(type));

            return ArchiveMethodsBy(types);
        }
        #endregion

        public async Task<bool> ArchiveAllMethod(Assembly[] assemblies)
        {
            IEnumerable<Type> types = TypeFinderExpansionMethods.GetTypeBy(assemblies).Where(type => typeof(IArchivedMethods).IsAssignableFrom(type));

            return await AsyncArchiveMethodsBy(types);
        }
        #region Async
        public async Task<bool> AsyncArchiveAllMethod(Assembly[] assemblies)
        {
            IEnumerable<Type> types = TypeFinderExpansionMethods.GetTypeBy(assemblies).Where(type => typeof(IArchivedMethods).IsAssignableFrom(type));

            return await AsyncArchiveMethodsBy(types);
        }
        #endregion
    }
}
