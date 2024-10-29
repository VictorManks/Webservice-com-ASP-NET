# Alert API

Este projeto é um servidor .NET para gerenciar alertas.

## Pré-requisitos

- .NET SDK 6.0 ou superior
- Git instalado

## Instruções de configuração

1. Clone o repositório:
```bash
git clone https://github.com/VictorManks/Webservice-com-ASP-NET.git
```

2. Navegue até a pasta do projeto:
 ```bash
 cd Webservice-com-ASP-NET/br.com.fiap.alert.api/
```

3. Execute o servidor em linux:
```bash
 nohup dotnet run
```

3. Execute o servidor em windows:
```bash
 start dotnet run
```

4. Navegar até a pasta do test:
 ```bash
 cd ..
 cd Webservice-com-ASP-NET/br.com.fiap.alert.test/
```

5. Executar test:
```bash
 dotnet test
```

❌ Em caso de erro tentem:
```bash
 dotnet nuget locals all --clear && dotnet restore
```
❌ Caso o erro persistir tente excluir o arquivo NuGet.config presente no diretório C:\Users<username>\AppData\Roaming\NuGet, e depois restaurá-lo utilizando o comando dotnet restore. Tente adicionar seu pacote após isso.
