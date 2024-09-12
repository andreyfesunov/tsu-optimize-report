# Инструкция по работе Backend

1. Если githook ругается на форматирование на backend, то, если не установлен dotnet-format, нужно вызвать его
   установку:

    ```bash
    dotnet tool install --global dotnet-format --version 5.1.250801
    ```

2. Запустить:

    ```bash
    dotnet-format BackendBase.sln
    ```