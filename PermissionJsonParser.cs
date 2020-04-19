using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DynamicPermissionList
{
    public class PermissionJsonParser
    {
        public static List<Permission> parse(string json)
            => parseJsonObject(JObject.Parse(json));

        private static List<Permission> parseJsonObject(JObject jObj, Permission parent = null)
        {
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
                        parseJsonObject((JObject) item.Value, permission);
                        break;
                    case JTokenType.Array:
                        parseJsonArray( (JArray) item.Value, permission);
                        break;
                }
            }

            return topPermissions;
        }

        private static void parseJsonArray(JArray jArr, Permission parent)
        {
            foreach (var item in jArr)
            {
                var permission = new Permission(item.Value<string>(), true);
                parent.addChild(permission);
            }
        }
    }
}