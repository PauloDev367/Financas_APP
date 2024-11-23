## Rodando o projeto com docker

---

### 1. **Identificar a Rede Criada pelo Docker Compose**
Quando você executa `docker-compose up --build`, o Docker cria automaticamente uma rede para os containers. Para listar as redes disponíveis, use:

```bash
docker network ls
```

Você verá algo como:

```plaintext
NETWORK ID     NAME                    DRIVER    SCOPE
abcd1234       myproject_default       bridge    local
efgh5678       bridge                  bridge    local
```

Encontre a rede relacionada ao seu projeto (normalmente no formato `<nome-do-diretório>_default`).

---

### 2. **Conectar o SQL Server à Rede**
Use o comando abaixo para conectar o container do SQL Server existente à rede do Docker Compose:

```bash
docker network connect <nome-da-rede> <nome-ou-id-do-container-sqlserver>
```

Substitua:
- `<nome-da-rede>` pelo nome da rede identificada no passo anterior (por exemplo, `myproject_default`).
- `<nome-ou-id-do-container-sqlserver>` pelo nome ou ID do container SQL Server em execução (você mencionou que o nome é `sqlserver`).

Exemplo:
```bash
docker network connect myproject_default sqlserver
```

---

### 3. **Atualizar a String de Conexão**
Garanta que os containers `webapi`, `webapi2` e `webapi3` usem a string de conexão correta para o SQL Server. O hostname deve ser o nome do container SQL Server, que neste caso é `sqlserver` (ou o nome configurado no `docker-compose.yml`).

Exemplo de string de conexão:
```plaintext
Server=sqlserver;Database=YourDatabase;User Id=sa;Password=YourPassword;
```

---

### 4. **Testar a Conexão**
Reinicie os containers do seu projeto para aplicar as mudanças de configuração sem recriar a instância do SQL Server:

```bash
docker-compose up -d
```

Agora, os serviços no `docker-compose` devem conseguir se comunicar com o container SQL Server existente.

---

### 5. **Verificar a Conexão**
Você pode verificar a conectividade entre os containers com o comando `docker exec` para testar, por exemplo, o `ping` entre os serviços:

```bash
docker exec -it <nome-do-container-webapi> ping sqlserver
```

Se o ping funcionar, a comunicação está estabelecida corretamente.