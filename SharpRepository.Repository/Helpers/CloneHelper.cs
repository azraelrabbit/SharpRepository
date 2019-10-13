using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime;
using System.Text;

namespace SharpRepository.Repository.Helpers
{
    public static class CloneHelper
    {

        //public static TOut DeepClone<TIn, TOut>(this TIn item)
        //{
        //    return Helpers.DeepClone<TIn, TOut>.Trans(item);
        //}

        public static T DeepClone<T>(this T item)
        {
            return Helpers.DeepClone<T, T>.Trans(item);
        }

        public static List<T> DeepClone<T>(this List<T> items)
        {
            var clonedList = new List<T>(items.Count);

            foreach (var item in items)
            {
                //var newItem = new T();
                //foreach (var propInfo in properties)
                //{
                //    // Don't try and set a value to a property w/o a setter
                //    if (propInfo.CanWrite)
                //        propInfo.SetValue(newItem, propInfo.GetValue(keyValuePair.Value, null), null);
                //}
                //use new deep clone by lambda compiler to improved performance most.
                var newItem = item.DeepClone();

                clonedList.Add(newItem);
            } 

            return clonedList;
        }
    }

    public static class DeepClone<TIn, TOut>
    {
        private static readonly Func<TIn, TOut> cache = GetFunc();
        private static Func<TIn, TOut> GetFunc()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            foreach (var item in typeof(TOut).GetProperties())
            {
                if (!item.CanWrite)
                    continue;

                MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

            return lambda.Compile();
        }

        public static TOut Trans(TIn tIn)
        {
            return cache(tIn);
        }
    }
}
