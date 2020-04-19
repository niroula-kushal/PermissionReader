using System.Collections.Generic;
using System.Linq;

namespace DynamicPermissionList
{
    public class Permission
    {
        public Permission parent;

        public List<Permission> children = new List<Permission>();

        public string name;

        public bool isLeaf;

        public Permission(string name, bool isLeaf = false)
        {
            this.name = name;
            this.isLeaf = isLeaf;
        }

        public void addChild(Permission childPermission)
        {
            children.Add(childPermission);
            childPermission.parent = this;
        }

        public string resolveName() => 
            parent == null ? 
                name : parent.resolveName() + "." + name;

        public int level => parent?.level + 1 ?? 1;
    }
}