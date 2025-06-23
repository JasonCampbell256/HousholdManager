# GitHub Copilot Instructions - Household Manager

## Project Overview

This is a **Household Management Application** built with:
- **ASP.NET Core 9.0** with **Blazor Server**
- **Entity Framework Core** with **SQLite** database
- **Bootstrap 5** for responsive UI
- **C#** with nullable reference types enabled

**Purpose**: Local-only family management application (no user authentication, designed for internal household use)

## Architecture & Patterns

### Project Structure
```
HouseholdManager/
├── Components/     # Blazor components (.razor files)
├── Data/          # Entity Framework DbContext
├── Models/        # Domain entities with relationships
├── Services/      # Business logic layer
├── Pages/         # Razor pages (host & layout)
├── wwwroot/       # Static assets
└── Program.cs     # DI container & app configuration
```

### Key Design Patterns
- **Service Layer Pattern**: Business logic separated into service classes
- **Repository Pattern**: Entity Framework with DbContext
- **Component-Based UI**: Blazor Server components with state management
- **Dependency Injection**: All services registered in Program.cs

## Domain Models & Entities

### Core Entities
- **Household**: Root aggregate containing all family data
- **FamilyMember**: People in the household with roles and relationships
- **Meal**: Recipes with many-to-many ingredient relationships
- **MealPlan**: Weekly meal planning with 21 entries (7 days × 3 meals)
- **Chore**: Task management with frequency and assignment
- **Note**: Shared household communications

### Key Relationships
- **Meal ↔ Ingredient**: Many-to-many via `MealIngredient` junction table
- **MealPlan → MealPlanEntry**: One-to-many with date/meal type
- **Household**: Contains all other entities as navigation properties

## Coding Conventions & Preferences

### C# Style
- Use **nullable reference types** (`<Nullable>enable</Nullable>`)
- Prefer **explicit null checks** and null-conditional operators
- Use **primary constructors** for simple classes when appropriate
- Follow **PascalCase** for public members, **camelCase** for private fields

### Blazor Components
- Use **@page** directive for routable components
- Implement **IDisposable** for components with event subscriptions
- Use **@inject** for dependency injection
- Prefer **@code** blocks at the bottom of components
- Use **StateHasChanged()** after async operations that modify UI state

### Entity Framework
- Use **Include()** for explicit loading of navigation properties
- Prefer **async/await** for all database operations
- Use **DbContext** directly in components (already registered as scoped)
- Follow **Code First** approach with data annotations

### UI/UX Patterns
- Use **Bootstrap 5** classes for styling
- Implement **responsive design** with mobile-first approach
- Use **Open Iconic** icons (`oi oi-*` classes)
- Modal dialogs for edit operations, inline forms for create operations
- Filter/search functionality where appropriate

## Component Examples

### Standard CRUD Component Pattern
```csharp
@page "/example"
@inject ServiceClass Service
@inject HouseholdContext Context

<PageTitle>Page Title - Household Manager</PageTitle>

// Add form, filter controls, data display

@code {
    private List<Entity> items = new();
    private bool showAddForm = false;
    private Entity newItem = new();
    
    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }
    
    private async Task LoadData()
    {
        // Load data with includes
    }
}
```

### Service Layer Pattern
```csharp
public class ExampleService
{
    private readonly HouseholdContext _context;
    
    public ExampleService(HouseholdContext context)
    {
        _context = context;
    }
    
    public async Task<List<Entity>> GetByHouseholdAsync(int householdId)
    {
        return await _context.Entities
            .Where(e => e.HouseholdId == householdId)
            .Include(e => e.NavigationProperty)
            .ToListAsync();
    }
}
```

## Current Features & Status

### ✅ Implemented Features
- **Advanced Ingredient System** with autocomplete and reusability
- **Weekly Meal Planning** with inline editing and meal suggestions
- **Family Member Management** with roles and age calculation
- **Chore Tracking** with frequency and assignment
- **Shared Notes** system for household communication
- **Responsive Design** with mobile and desktop layouts

### 🚧 Navigation Routes
All routes are defined in `NavMenu.razor`:
- `/` - Dashboard/Home
- `/meals` - Meal management with ingredients
- `/meal-planner` - Weekly meal planning calendar
- `/family-members` - Family member CRUD
- `/chores` - Chore management with filters
- `/notes` - Shared notes system
- Plus placeholders for: maintenance

## Database Context

- **Connection String**: SQLite with `household.db` file
- **Auto-Migration**: Database created automatically on startup
- **Scoped Lifetime**: DbContext registered as scoped service
- **Direct Injection**: Components inject HouseholdContext directly

## Development Guidelines

### When Adding New Features
1. Create domain model in `Models/` directory
2. Add DbSet to `HouseholdContext`
3. Create service class in `Services/` directory
4. Register service in `Program.cs`
5. Create Blazor component in `Components/` directory
6. Add navigation link in `NavMenu.razor`
7. **Update DEVELOPMENT_LOG.md** with implementation details

### Code Generation Preferences
- Generate **complete CRUD operations** for new entities
- Include **responsive Bootstrap markup** with mobile considerations
- Implement **proper error handling** and user feedback
- Add **loading states** for async operations
- Use **consistent naming conventions** matching existing code

### Testing & Validation
- Include **model validation** with data annotations
- Handle **async operation errors** gracefully
- Implement **optimistic UI updates** where appropriate
- Test **mobile responsiveness** for all components

## Documentation Requirements

### Development Log
- **ALWAYS read DEVELOPMENT_LOG.md** before making significant changes
- **ALWAYS update DEVELOPMENT_LOG.md** after implementing new features
- Document implementation details, architecture decisions, and technical notes
- Update feature status from 🚧 (pending) to ✅ (implemented)
- Add new session entries with date and detailed changes
- Update "Current Capabilities" and "Known Issues" sections as needed

### Log Structure to Follow
```markdown
### **Session X (Date) - Feature Name**
1. **Technical Implementation**
   - Specific changes made
   - Architecture decisions
   - Database schema updates

2. **UI/UX Features**
   - Component structure
   - User interaction patterns
   - Responsive design considerations

3. **Service Integration**
   - Business logic implementation
   - Dependency injection registration
   - Integration with existing services
```

### What to Document
- **New entities and relationships** added to domain model
- **Service layer changes** including new methods and patterns
- **UI component architecture** and user interaction patterns
- **Database schema updates** including migrations needed
- **Technical decisions** and why they were made
- **Known limitations** or technical debt introduced
- **Next steps** or follow-up work needed

## Dependencies & Packages

### Core Packages
- `Microsoft.AspNetCore.Components.Web` (9.0.6)
- `Microsoft.EntityFrameworkCore.Sqlite` (9.0.6)
- `Microsoft.EntityFrameworkCore.Design` (9.0.6)

### UI Framework
- Bootstrap 5 (included via CDN)
- Open Iconic icons
- Custom CSS in `wwwroot/css/site.css`

---

**Application URL**: http://localhost:5001  
**Database**: SQLite file at `HouseholdManager/household.db`  
**Target Framework**: .NET 9.0