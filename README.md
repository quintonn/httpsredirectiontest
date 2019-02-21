# https Redirection Test
To test https redirection in .net core where i think is a bug or an undocumented requirement

### Expected behaviour:
- [x] Browse to http://localhost:5020/basic/test1 and see "http method -> Ok, it works".
- [ ] Browse to http://localhost:5020/basic/test2 and be redirected to https://localhost:5021/basic/test2 and see "httpS method -> Ok, it works" 

The above all works if I include this in my Startup file:

```
services.AddMvcCore(x =>
{
    x.SslPort = 5021; // Problem: I have to configure this port here and in Program.cs. Why is this necessary?
});
```

But if I replace it with the following code, it doesn't work:
```
services.AddMvc();
```
This code should not be required, what's the point of all the https redirection code?


## Another Issue:
This code seems to do absolutely nothing (with or without previous piece of code, never redirects to this port).
```
services.AddHttpsRedirection(x =>
{
    x.HttpsPort = 5022;
});
```
What is the point of this code, why does it exist?
