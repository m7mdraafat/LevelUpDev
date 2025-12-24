# LevelUpDev

A gamified LeetCode community platform for ~200 members with quests, squads, leaderboards, and achievements.

## 🏗️ Architecture

```
LevelUpDev/
├── LevelUpDev.Domain/           # Core domain (entities, enums, interfaces)
├── LevelUpDev.Application/      # Application layer (DTOs, services, validators)
├── LevelUpDev.Infrastructure/   # Infrastructure (Cosmos DB repositories)
└── LevelUpDev.Api/              # ASP.NET Core Web API
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Azure Cosmos DB Emulator](https://learn.microsoft.com/azure/cosmos-db/emulator) (for local development)
- [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli) (for deployment)

### Local Development

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-org/LevelUpDev.git
   cd LevelUpDev
   ```

2. **Set up local settings**
   ```bash
   # Copy the template
   cp LevelUpDev.Api/appsettings.Development.json.template LevelUpDev.Api/appsettings.Development.json
   ```

3. **Start Cosmos DB Emulator** (or use Azure Cosmos DB)

4. **Run the application**
   ```bash
   dotnet run --project LevelUpDev.Api
   ```

5. **Access Swagger UI**
   ```
   https://localhost:5001/swagger
   ```

## ☁️ Azure Deployment

### Required Azure Resources

| Resource | Purpose |
|----------|---------|
| **Azure Cosmos DB** | NoSQL database for all data |
| **Azure App Service** | Host the Web API |
| **Managed Identity** | Secure connection to Cosmos DB |

### Enable Managed Identity

1. Go to your **App Service** → **Identity** → **System assigned** → **On**
2. Go to **Cosmos DB** → **Access Control (IAM)** → **Add role assignment**
3. Assign **Cosmos DB Built-in Data Contributor** role to your App Service's managed identity

### Configure App Settings

In Azure Portal → App Service → **Configuration** → **Application settings**:

| Setting | Value |
|---------|-------|
| `CosmosDb__EndpointUri` | `https://your-cosmos.documents.azure.com:443/` |
| `CosmosDb__DatabaseName` | `LevelUpDev` |

> ⚠️ **No connection string needed!** Managed Identity handles authentication.

## 🔄 GitHub Actions CI/CD

### Setup

1. **Get Publish Profile**
   - Azure Portal → App Service → **Deployment Center** → **Manage publish profile** → Download

2. **Add GitHub Secret**
   - GitHub → Repository → **Settings** → **Secrets and variables** → **Actions**
   - Add secret: `AZURE_WEBAPP_PUBLISH_PROFILE` (paste the XML content)

3. **Update Workflow**
   - Edit `.github/workflows/deploy.yml`
   - Replace `your-app-service-name` with your actual App Service name

### Workflows

| Workflow | Trigger | Purpose |
|----------|---------|---------|
| `deploy.yml` | Push to `main` | Build & deploy to Azure |
| `pr-validation.yml` | PR to `main`/`develop` | Validate builds & tests |

## 📊 Cosmos DB Containers

| Container | Partition Key | Purpose |
|-----------|---------------|---------|
| `users` | `/id` | User profiles |
| `stats` | `/userId` | User statistics |
| `squads` | `/id` | Squad data |
| `achievements` | `/userId` | User achievements |
| `leaderboards` | `/type` | Leaderboard entries |
| `challenges` | `/date` | Daily challenges |
| `activities` | `/date` | Activity feed |
| `notifications` | `/userId` | User notifications |
| `communitygoals` | `/weekStart` | Community goals |

## 🔑 Features

- **Quest World Map**: DSA (35 levels), Database (5), System Design (5), Maths (7)
- **Streak Wars**: Daily streak tracking with freeze tokens
- **Squads**: Team-based competitions
- **Smart Leaderboards**: Multiple categories (streaks, XP, problems, consistency)
- **Daily Challenges**: Community problems with bonus points
- **Achievement System**: Badges with rarity tiers
- **Personal Analytics**: Dashboard with progress tracking
- **Smart Notifications**: Streak reminders, achievements, squad updates

## 📝 License

MIT License