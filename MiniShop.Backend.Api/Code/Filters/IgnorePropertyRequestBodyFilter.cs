using Microsoft.OpenApi.Models;
using MiniShop.Backend.Api.Code.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace MiniShop.Backend.Api.Code.Filters
{
    /// <summary>
    /// "multipart/form-data" 表单提交模型，注释、默认值、隐藏属性处理
    /// </summary>
    public class IgnorePropertyRequestBodyFilter : IRequestBodyFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestBody"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiRequestBody requestBody, RequestBodyFilterContext context)
        {
            if (requestBody.Content.Keys.Contains("multipart/form-data"))
            {
                var pro = typeof(SchemaRepository).GetField("_reservedIds", BindingFlags.NonPublic | BindingFlags.Instance);
                if (pro == null)
                    return;

                //var schemaTypes = (Dictionary<Type, string>)pro.GetValue(context.SchemaRepository);
                var pros = requestBody.Content["multipart/form-data"].Schema.Properties;

                foreach (var schema in pros)
                {
                    //FromForm 接口参数特性导致 s.PropertyInfo() 为空报错，无法在线打开 api 文档！ 这里捕获一下
                    try
                    {
                        var s = context.FormParameterDescriptions.FirstOrDefault(p => p.Name == schema.Key);
                        var displayAttr = s?.ModelMetadata.DisplayName;
                        var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(s.PropertyInfo(), typeof(DescriptionAttribute));
                        var defaultValue = (DefaultValueAttribute)Attribute.GetCustomAttribute(s.PropertyInfo(), typeof(DefaultValueAttribute));
                        var ignoreProperties = (IgnorePropertyAttribute)Attribute.GetCustomAttribute(s.PropertyInfo(), typeof(IgnorePropertyAttribute));

                        if (ignoreProperties != null)
                        {
                            pros.Remove(schema.Key);
                            continue;
                        }

                        if (defaultValue != null)
                        {
                            schema.Value.Default = new Microsoft.OpenApi.Any.OpenApiString(defaultValue.Value?.ToString());
                        }

                        if (displayAttr != null)
                        {
                            if (schema.Value.Reference != null)
                            {
                                var values = context.SchemaRepository.Schemas[schema.Value.Reference.Id];
                                schema.Value.Enum = values.Enum;
                                schema.Value.Type = values.Type;
                                schema.Value.Reference = null;
                            }
                            schema.Value.Description = displayAttr;
                            continue;
                        }
                        if (descAttr != null && descAttr.Description.NotNull())
                        {
                            schema.Value.Description = descAttr.Description;
                        }
                    }
                    catch (Exception)
                    {
                        //throw;
                    }
                }
            }

        }
    }
}
