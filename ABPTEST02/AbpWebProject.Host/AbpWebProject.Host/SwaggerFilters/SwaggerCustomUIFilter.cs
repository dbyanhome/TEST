using Microsoft.AspNetCore.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Xml;

namespace AbpWebProject.Host.SwaggerFilters
{
    public sealed class SwaggerCustomUIFilter : IDocumentFilter
    {
        /// <summary>
        /// JSON 字段缓存项
        /// </summary>
        private readonly ConcurrentDictionary<string, string> _cacheDictionary = new ConcurrentDictionary<string, string>();

        private readonly IHostingEnvironment _environment;

        public SwaggerCustomUIFilter(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var _xmlDirPath = Path.Combine(_environment.ContentRootPath, "XMLDocument");

            foreach (var filePath in Directory.GetFiles(_xmlDirPath))
            {
                BuildSwaggerDictionary(filePath);
            }

            swaggerDoc.Extensions.TryAdd("ControllerDescription", _cacheDictionary);
        }

        /// <summary>
        /// 构建每个 XML 文档的数据字典
        /// </summary>
        /// <param name="xmlPath">XML 文档路径</param>
        private void BuildSwaggerDictionary(string xmlPath)
        {
            if (!File.Exists(xmlPath)) return;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);
            foreach (XmlNode node in xmlDocument.SelectNodes("//member"))
            {
                // 判定编译出来的类型是否是应用服务层
                string type = node.Attributes["name"].Value;

                if (type.StartsWith("T:") && type.Contains("ApplicationService") &&
                    !type.Contains("T:HKERP.HKERPAppServiceBase") &&
                    !type.Contains("T:HKERP.Net.MimeTypes.MimeTypeNames"))
                {
                    // 获取到具体的应用服务描述信息
                    XmlNode summaryNode = node.SelectSingleNode("summary");

                    // 构建字典 Key
                    string[] nameArray = type.Split('.');
                    string dictKey = nameArray[nameArray.Length - 1];
                    // 判断这个类型的结尾是否以 AppService 命名
                    if (dictKey.IndexOf("ApplicationService", dictKey.Length - "ApplicationService".Length, StringComparison.Ordinal) > -1)
                    {
                        dictKey = dictKey.Substring(0, dictKey.Length - "ApplicationService".Length);
                    }

                    if (summaryNode != null && !string.IsNullOrWhiteSpace(summaryNode.InnerText) &&
                        !_cacheDictionary.ContainsKey(dictKey))
                    {
                        _cacheDictionary.TryAdd(dictKey, summaryNode.InnerText.Trim());
                    }
                }
            }
        }
    }
}
