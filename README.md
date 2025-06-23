# Household Management Application

A local-only household management application written in C# using Entity Framework Core. This app is designed for internal use by one family and does not require user accounts or internet access.

## Features

### Core Functionality
- **Meal Planning**: Store common meals with descriptions, tags, and ingredients. Track when meals were last cooked and create weekly meal plans.
- **Chore Management**: Define chores with frequency (daily, weekly, monthly, as-needed) and assign to family members.
- **Maintenance Tracking**: Track home maintenance tasks with automatic due date calculation.
- **Shared Notes**: Family members can create and read general-purpose notes.
- **Grocery Lists**: Create named grocery lists with items, quantities, categories, and checkboxes.
- **Expense Tracking**: Log expenses with amount, date, category, and description.
- **Document Management**: Track files related to home or appliance maintenance.
- **Pet Management**: Track pets and their care tasks.

## Domain Model

The system is built around a `Household` entity that contains all relevant data for a family:

- **Household**: Root entity containing all family data
- **FamilyMember**: People in the household with optional roles
- **Meal**: Common meals with ingredients and cooking history
- **MealPlan**: Weekly meal planning with daily assignments
- **MealPlanEntry**: Individual meal assignments within plans
- **Chore**: Household tasks with frequency and assignments
- **MaintenanceTask**: Home maintenance with automatic scheduling
- **Note**: Shared family notes
- **GroceryList**: Shopping lists with items
- **GroceryItem**: Individual items in grocery lists
- **Expense**: Financial tracking
- **Document**: File references for household documents
- **Pet**: Pet information
- **PetCareTask**: Pet care activities

## Technology Stack

- **Language**: C# (.NET 9.0)
- **Database**: SQLite with Entity Framework Core
- **ORM**: Entity Framework Core 9.0
- **Storage**: Local SQLite database file

## Project Structure

```
HouseholdManager/
├── Models/           # Domain entities
├── Data/            # Entity Framework DbContext
├── Services/        # Business logic services
├── Program.cs       # Application entry point
└── household.db     # SQLite database file
```

## Getting Started

1. **Prerequisites**: .NET 9.0 SDK
2. **Clone and Build**:
   ```bash
   git clone <repository-url>
   cd HouseholdManager
   dotnet build
   ```
3. **Run**:
   ```bash
   dotnet run
   ```

The application will automatically create the SQLite database on first run.

## Services

The application includes several service classes for business logic:

- **HouseholdService**: Manage household creation and retrieval
- **MealService**: Meal management and cooking history
- **ChoreService**: Chore tracking with automatic due dates
- **MaintenanceService**: Home maintenance scheduling

## Database

Uses SQLite for local storage with the following key features:
- Automatic database creation
- Proper foreign key relationships
- Cascade deletes for data integrity
- Nullable relationships where appropriate

## Usage Examples

The application demonstrates creating:
- Family members with roles
- Meals with ingredients and tags
- Chores assigned to family members
- Maintenance tasks with automatic scheduling
- Expenses with categories
- Shared family notes

## Future Enhancements

Potential additions could include:
- Web-based UI
- Meal plan templates
- Recipe import/export
- Photo attachments for documents
- Reporting and analytics
- Data backup/restore functionality

## License

This is a personal/family use application. Modify as needed for your household management requirements.