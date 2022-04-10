# MiniShop.Backend.Api


## 使用 dockerfile 构建部署

```shell
# 创建镜像
docker build -t minishopbackendapi -f Dockerfile .

# 启动容器
docker run -d -p 15201:80 --restart=always -v D:/dockervolumes/minishopbackendapi/appsettings.json:/app/appsettings.json -v D:/dockervolumes/minishopbackendapi/log:/app/log --name minishopbackendapi minishopbackendapi
```



