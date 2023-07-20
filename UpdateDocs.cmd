dotnet tool install xmldocmd -g
dotnet build -c Debug SharpEval.sln
xmldocmd bin\Debug\SharpEval.Core.dll Docs