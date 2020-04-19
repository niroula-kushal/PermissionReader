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
            Console.WriteLine(new String('*', 50));
            permissions.ForEach(renderPermission);
            Console.WriteLine(new String('*', 50));
        }

        private static void renderPermission(Permission permission)
        {
            Console.Write(new String('-', permission.level == 1 ? 0 : 4));
            Console.Write(permission.name);
            if (permission.isLeaf)
            {
                Console.Write("  ");
                Console.Write($"[{permission.resolveName()}]");
            }

            Console.Write("\n");
            if (!permission.isLeaf)
            {
                permission.children.ForEach((child) => {
                    Console.Write(new String(' ', (permission.level - 1) * 5));
                    Console.Write("|");
                    renderPermission(child);
                });
            }
        }
    }
}