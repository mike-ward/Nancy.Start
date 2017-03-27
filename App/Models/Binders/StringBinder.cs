using System;
using System.IO;
using Nancy;
using Nancy.ModelBinding;

namespace App.Models.Binders
{
    public class StringBinder : IModelBinder
    {
        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration, params string[] blackList)
        {
            using (var rdr = new StreamReader(context.Request.Body))
            {
                return rdr.ReadToEnd();
            }
        }

        public bool CanBind(Type modelType)
        {
            return modelType == typeof(string);
        }
    }
}