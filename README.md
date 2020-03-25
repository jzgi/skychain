# The CloudUn Framework & Server  

CloudUn is a framework & server for federated cloud. It can be either embedded in application, or deployed as a standalone server. 
It is combination of a web application framework and a federated blockchain implementation.

The characteristics:

* A design pattern of contextual web programming.
* Lightweight -- about 200K in its size. 
* High performance -- Exploit async I/O, built-in optimized caching. 
* Unified data operation -- for various data formats.
* Built-in implementation of a high performance federated blockchain. 

<pre>
dotnet add package CloudUn --version 1.1.0
</pre>

# Dependencies

The framework specifically uses the following open-source libraries. Many thanks to the great teams respectively!

| ![kestrel](https://github.com/skyiah/cloudun/tree/master/Docs/netcore.png) | ![npgsql](https://github.com/skyiah/cloudun/tree/master/Docs/postgresql.png) |
| ---- | ----- |
| [kestrel](https://github.com/aspnet/AspNetCore) | [npgsql](http://www.npgsql.org) |
| .NET's built-in web engine | .NET access to PostgreSQL |
