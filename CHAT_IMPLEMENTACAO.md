# Implementação do Chat Assíncrono (SignalR)

Este documento detalha as alterações realizadas para implementar o sistema de chat assíncrono para campanhas, conforme solicitado.

## 1. Modelo de Dados (Backend)

### 1.1. Nova Tabela `CHAT_CAMPANHA`

Foi criado o modelo `ChatCampanha.cs` para representar as mensagens do chat.

| Campo | Tipo | Descrição |
| :--- | :--- | :--- |
| `Id` | `int` | Chave primária. |
| `UserId` | `string` | Chave estrangeira para a tabela `Usuario` (quem enviou a mensagem). |
| `IdCampanha` | `int` | Chave estrangeira para a tabela `CampanhaMesa` (o grupo de chat). |
| `DataHora` | `DateTime` | Data e hora do envio da mensagem. |
| `Texto` | `string` | Conteúdo da mensagem (máx. 1000 caracteres). |
| `TipoUsuario` | `int` | Indica o papel do usuário no chat: `1` para Mestre, `0` para Jogador. |

### 1.2. Atualização do Contexto do Banco de Dados

O modelo `ChatCampanha` foi adicionado ao `AppDbContext.cs` para que o Entity Framework Core possa gerenciar a tabela:

```csharp
// TCC-Nexos/Nexos/Data/AppDbContext.cs
public DbSet<ChatCampanha> ChatsCampanha { get; set; }
```

## 2. Comunicação em Tempo Real (SignalR)

### 2.1. `ChatHub.cs`

Foi criado o `ChatHub.cs` para gerenciar a comunicação em tempo real usando SignalR.

- **`SendMessage(campanhaId, userId, message, tipoUsuario)`**: Salva a mensagem no banco de dados e a transmite para todos os clientes no grupo da campanha.
- **`JoinCampanha(campanhaId)`**: Adiciona o cliente à um grupo específico (`Campanha_{campanhaId}`) para receber mensagens apenas daquela campanha.

### 2.2. Configuração do `Program.cs`

O SignalR foi configurado no pipeline do ASP.NET Core:

```csharp
// TCC-Nexos/Nexos/Program.cs
builder.Services.AddSignalR();
// ...
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<Nexos.Hubs.ChatHub>("/chatHub");
    // ...
});
```

## 3. Interface do Usuário (Frontend)

### 3.1. `ChatController.cs`

O `ChatController` foi criado para:

1.  Carregar a `CampanhaMesa` e verificar se o usuário logado é o Mestre ou um Jogador.
2.  Carregar as últimas 50 mensagens do chat para exibição inicial.
3.  Passar os dados para a View através do `ChatCampanhaViewModel`.

### 3.2. `ChatCampanhaViewModel.cs`

Um ViewModel simples para transportar os dados necessários para a View:

```csharp
// TCC-Nexos/Nexos/ViewModels/ChatCampanhaViewModel.cs
public class ChatCampanhaViewModel
{
    public CampanhaMesa Campanha { get; set; }
    public bool IsMestre { get; set; }
    public string UserId { get; set; }
    public List<ChatCampanha> Mensagens { get; set; }
}
```

### 3.3. View `Campanha.cshtml`

A View `TCC-Nexos/Nexos/Views/Chat/Campanha.cshtml` implementa a interface do chat:

-   Exibe o histórico de mensagens.
-   Usa o SignalR JavaScript Client (`~/lib/signalr/dist/browser/signalr.js`) para estabelecer a conexão com o `ChatHub`.
-   O script JavaScript lida com o envio de mensagens (`connection.invoke("SendMessage", ...)`), a entrada no grupo (`connection.invoke("JoinCampanha", ...)`), e a recepção de novas mensagens (`connection.on("ReceiveMessage", ...)`).

### 3.4. Integração na View de Detalhes

Um botão "Acessar Chat" foi adicionado à view `TCC-Nexos/Nexos/Views/Home/MesaDetalhes.cshtml` para levar o usuário à nova tela de chat:

```html
<a asp-controller="Chat" asp-action="Campanha" asp-route-id="@Model.ID_Campanha" class="btn btn-success btn-lg px-5 py-3 rounded-pill">
    Acessar Chat
</a>
```

## Próximos Passos

Para que o chat funcione completamente, é necessário:

1.  **Executar a Migração do Banco de Dados**: Criar e aplicar uma nova migração para adicionar a tabela `CHAT_CAMPANHA` ao banco de dados.
2.  **Testar a Funcionalidade**: Verificar se a comunicação em tempo real e o salvamento no banco estão funcionando corretamente.

**Observação**: A lógica de permissão no `ChatController` é básica (Mestre ou qualquer usuário logado). Em um sistema de produção, seria necessário uma tabela de relacionamento Campanha-Jogador para verificar se o usuário é um participante ativo da campanha.
