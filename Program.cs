using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DynamicPermissionList
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText(@"permissions.json");
            var permissions = parseJsonObject(json);
            foreach (var permission in permissions)
            {
                renderPermission(permission);
            }
        }

        private static void renderPermission(Permission permission)
        {
            Console.Write(new String('\t', permission.level - 1));
            Console.Write(permission.name);
            if (permission.isLeaf)
            {
                Console.Write("\t");
                Console.Write($"[{permission.resolveName()}]");
            }

            Console.Write("\n");
            if (!permission.isLeaf)
            {
                foreach (var child in permission.children)
                {
                    renderPermission(child);
                }
            }
        }

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