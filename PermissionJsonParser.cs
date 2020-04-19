using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DynamicPermissionList
{
    public class PermissionJsonParser
    {
        public static List<Permission> parse(string json)
            => parseJsonObject(json);

        private static List<Permission> parseJsonObject(string json, Permission parent = null)
        {
            var jObj = JObject.Parse(json);
            var topPermissions = new List<Permission>();
            foreach (var item in jObj)
            {
                var permission = new Permission(item.Key);
                if (parent != null)
                {
                    parent.addChild(permission);
                }
                else
                {
                    topPermissions.Add(permission);
                }

                switch (item.Value.Type)
                {
                    case JTokenType.Object:
                        parseJsonObject(item.Value.ToString(), permission);
                        break;
                    case JTokenType.Array:
                        parseJsonArray(item.Value.ToString(), permission);
                        break;
                }
            }

            return topPermissions;
        }

        private static void parseJsonArray(string json, Permission parent)
        {
            var jArr = JArray.Parse(json);
            foreach (var item in jArr)
            {
                var permission = new Permission(item.Value<string>(), true);
                parent.addChild(permission);
            }
        }
    }
}