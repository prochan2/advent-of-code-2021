# Usage: AddTask.sh <source day> <source task> <destination day> <new task>
mkdir -p src/day$3/task$4
cp src/day$1/task$2/Day$1.Task$2.csproj src/day$3/task$4/Day$3.Task$4.csproj
cp src/day$1/task$2/Program.cs src/day$3/task$4/Program.cs
dotnet sln add --in-root src/day$3/task$4/Day$3.Task$4.csproj
