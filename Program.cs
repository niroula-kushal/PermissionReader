using System;
using System.IO;

namespace DynamicPermissionList
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText(@"permissions.json");
            var permissions = PermissionJsonParser.parse(json);
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
    }
}