# JSONTools
Tools to make working with JSON in dotnet less troublesome.

----
## ExcelMatrix

Serialize a `List<List<object>>` to JSON:
```c#
var values = new List<List<object>>
{
    new List<object> {"header1", "header2", "header3", "header4"},
    new List<object> {"r1v1", "r1v2", "r1v3", 1},
    new List<object> {"r2v1", "r2v2", "r2v3", 2},
    new List<object> {"r3v1", "r3v2", "r3v3", 3},
};

var result = ExcelMatrix.ToJson(values);
```

Deserialize a `List<List<object>>` to `YourType`:
```c#
var values = new List<List<object>>
{
    new List<object> {"header1", "header2", "header3", "header4"},
    new List<object> {"r1v1", "r1v2", "r1v3", 1},
    new List<object> {"r2v1", "r2v2", "r2v3", 2},
    new List<object> {"r3v1", "r3v2", "r3v3", 3},
};

var result = ExcelMatrix.Deserialize<YourType>(values);
```