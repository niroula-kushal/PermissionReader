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

        public string label;

        public Permission(string name, string label = null, bool isLeaf = false)
        {
            this.name = name;
            this.isLeaf = isLeaf;
            this.label = label;
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