# PermissionReader

C# implementation of permission reader.

Reads a JSON representation of permission listing and creates permission objects accordingly.

```c#
string json = getJsonFromSomeWhere();
List<Permission> permissions = PermissionJsonParser.parse(json);

// Gives the parent permission of a permission. Null for top level permission
var parent = permission.parent;

// Gives the permissions nested under this permission
var childrenPermissions = permission.children;

// Returns true if the permission is a leaf node, else false
bool isLeaf = permission.isLeaf;

/* 
* Returns the level of permission.
{
    "Inventory": {
      "Item" : ["Create"]
   }
}

For permission representing "Inventory", permission.level = 1
For "Item", permission.level = 2
*/
//
//
int level = permission.level;

// Gives the name of the permission. "Inventory", "Item" ...
string name = permission.name;

// Gives permission name appended with parent permission "Inventory.Item", "Inventory.Item.Create" etc
string resolvedName = permission.resolveName();
```

