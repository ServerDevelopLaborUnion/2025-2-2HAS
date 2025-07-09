using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MehodArchive
{
    public static class MethodArchiveHelper
    {
        public class ArchiveMethodInfo
        {
            public string name;
            public MethodInfo method;

            public ArchiveMethodInfo(string name, MethodInfo method)
            {
                this.name = name;
                this.method = method;
            }
        }

        public static IEnumerable<ArchiveMethodInfo>? GetArchiveStaticMehods(Type type)
        {
            return type?.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Select(method => (method: method, attribute: method.GetCustomAttribute<ArchiveMethodAttribute>()))
                .Where(infoes => infoes.method is not null && infoes.attribute is not null)
                .Select(infoes => new ArchiveMethodInfo(
                    name: infoes.attribute!.methodName,
                    method: infoes.method
                    ));
        }

        public static Delegate MethodInfoCastToDelegate(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
                throw new InvalidOperationException($"Recceived method information is not static. method name : {methodInfo.Name}");

            ParameterInfo[] parameters = methodInfo.GetParameters();
            IEnumerable<ParameterExpression> parameterExpressions = parameters.Select(parameter => Expression.Parameter(parameter.ParameterType));

            MethodCallExpression methodCallExpression = Expression.Call(null, methodInfo, parameterExpressions);
            Type? funcType;

            if (methodInfo.ReturnType == typeof(void))
                funcType = Expression.GetActionType(parameterExpressions.Select(pe => pe.Type).ToArray());
            else
                funcType = Expression.GetFuncType(parameterExpressions.Select(pe => pe.Type).Concat(new Type[] { methodInfo.ReturnType }).ToArray());

            LambdaExpression lambdaExpression = Expression.Lambda(funcType, methodCallExpression, parameterExpressions);
            return lambdaExpression.Compile();
        }
    }
}
