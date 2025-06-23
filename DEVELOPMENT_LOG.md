# Household Management App - Development Log

## Project Overview

A local-only household management application built with **Blazor Server** and **Entity Framework Core**. Designed for internal family use without requiring user accounts or internet access.

**Current Date:** June 22, 2025  
**Status:** ✅ Weekly Meal Planning System Implemented  
**Application URL:** http://localhost:5001

---

## 🏗️ Architecture

- **Frontend:** Blazor Server (C#)
- **Backend:** ASP.NET Core 9.0
- **Database:** SQLite with Entity Framework Core
- **Styling:** Bootstrap 5 + Custom CSS
- **Deployment:** Local development server

---

## 📁 Project Structure

```
HouseholdManager/
├── Components/          # Blazor UI Components
├── Data/               # Entity Framework DbContext
├── Models/             # Domain Models (Including Advanced Meal Planning)
├── Services/           # Business Logic Services
├── Pages/              # Razor Pages (Host & Layout)
├── wwwroot/            # Static Assets (CSS, JS)
├── Program.cs          # Application Entry Point
└── _Imports.razor      # Global Using Statements
```

---

## 🎯 Features Implemented

### ✅ **Core Infrastructure**
- [x] Blazor Server setup with dependency injection
- [x] Entity Framework Core with SQLite
- [x] Bootstrap 5 responsive design
- [x] Navigation sidebar with routing
- [x] Global error handling

### ✅ **Advanced Ingredient System**
- [x] **Ingredient Entity** - Standalone ingredients with reusability
- [x] **MealIngredient Junction** - Many-to-many with quantities & notes
- [x] **Autocomplete Search** - Type-ahead ingredient selection
- [x] **Auto-Creation** - New ingredients created on-the-fly
- [x] **Full CRUD Operations** - Create, read, update, delete meals with ingredients

### ✅ **Weekly Meal Planning System**
- [x] **MealPlan Entity** - Weekly meal plans with date ranges
- [x] **MealPlanEntry Entity** - Individual meal assignments (Breakfast/Lunch/Dinner)
- [x] **MealPlanService** - Complete meal planning business logic
- [x] **Weekly Calendar View** - Interactive Sunday-Saturday meal planner
- [x] **Navigation Controls** - Previous/Current/Next week navigation
- [x] **Meal Assignment** - Click-to-edit with search and custom meal options
- [x] **Previous Week Reference** - Always shows previous week for inspiration

### ✅ **UI Components**
- [x] **Home Dashboard** - Overview cards with statistics
- [x] **Meals Management** - Complete CRUD with sophisticated ingredient system
- [x] **Meal Planner** - Interactive weekly calendar with meal assignments
- [x] **Family Members** - Member management with roles/ages
- [x] **Notes** - Shared household notes system
- [x] **Navigation Menu** - Sidebar with all sections including meal planner

### ✅ **Database Models**
Enhanced domain models with complete meal planning system:
- Household (root entity)
- Ingredient, MealIngredient (advanced ingredient system)
- **MealPlan** - Weekly meal plans with household association
- **MealPlanEntry** - Individual meal assignments with date/time/meal references
- Meal (with ingredients and planning integration)
- FamilyMember, Chore, MaintenanceTask
- Note, GroceryList/Item, Expense, Document
- Pet, PetCareTask

### ✅ **Services Layer**
- [x] HouseholdService - Core household operations
- [x] IngredientService - Full ingredient CRUD, search, categories
- [x] MealService - Enhanced with ingredient relationship management
- [x] **MealPlanService** - Complete meal planning operations
  - [x] Weekly plan creation and retrieval
  - [x] Meal assignment and updates
  - [x] Navigation between weeks
  - [x] Custom meal support
  - [x] Integration with existing meal library
- [x] ChoreService - Chore management (partially implemented)
- [x] MaintenanceService - Home maintenance tasks

---

## 🚧 Features Pending Implementation

### 🔄 **UI Components Not Yet Created**
- [ ] **Ingredients Management Page** - Dedicated ingredient library interface
- [ ] **Chores Page** - Exists but needs backend integration
- [ ] **Maintenance Tasks** - Create component
- [ ] **Grocery Lists** - Create component (can now leverage meal plans)
- [ ] **Expenses** - Create component  
- [ ] **Pets** - Create component
- [ ] **Documents** - Create component

### 🔄 **Advanced Features Enabled by Meal Planning**
- [ ] **Shopping List Generation** - Auto-generate from weekly meal plans
- [ ] **Ingredient Aggregation** - Sum ingredients across planned meals
- [ ] **Meal Plan Templates** - Save and reuse common weekly patterns
- [ ] **Recipe Scaling** - Adjust quantities for serving sizes
- [ ] **Nutritional Tracking** - Weekly nutrition analysis from planned meals
- [ ] **Meal Plan History** - View past weeks and cooking success rates

---

## 🏃‍♂️ How to Run

```bash
cd /home/jasoncampbell/Developer/home-manager/HouseholdManager
dotnet run
```

Navigate to: **http://localhost:5001**

---

## 🛠️ Recent Development History

### **Session 3 (June 22, 2025) - Weekly Meal Planning System**
1. **Created MealPlanService**
   - Automatic weekly meal plan creation with Sunday-Saturday structure
   - Smart meal assignment with existing meal integration
   - Week navigation (previous/current/next) with proper date handling
   - Support for both existing meals and custom meal names
   - Integration with existing meal tracking (auto-marks as cooked)

2. **Built MealPlanner Component**
   - **Interactive Weekly Calendar** - Full Sunday-Saturday grid layout
   - **Responsive Table Design** - Bootstrap table with proper mobile handling
   - **Click-to-Edit Interface** - Inline editing for any meal slot
   - **Meal Search Integration** - Autocomplete search of existing meals
   - **Custom Meal Support** - Type any meal name for flexibility
   - **Previous Week Reference** - Always shows previous week below current
   - **Today Highlighting** - Current day row highlighted in yellow
   - **Navigation Controls** - Previous/Current/Next week buttons

3. **Database Integration**
   - Leveraged existing MealPlan and MealPlanEntry models
   - Automatic creation of 21 entries per week (7 days × 3 meals)
   - Proper foreign key relationships with meals and households
   - Cascade delete handling for data integrity

4. **Service Registration & Navigation**
   - Added MealPlanService to dependency injection in Program.cs
   - Integrated meal planner into navigation menu with calendar icon
   - Positioned logically between Meals and Family Members

5. **Advanced UX Features**
   - **Smart Date Handling** - Automatic Sunday week start calculation
   - **Meal Suggestion System** - Shows relevant meals as you type
   - **Visual Feedback** - Clear indication of assigned vs. empty slots
   - **Previous Week Context** - Compact view of last week's meals
   - **Flexible Input** - Switch between meal library and custom text

### **Session 2 (June 22, 2025) - Advanced Ingredient System**
1. **Refactored Ingredient Architecture**
   - Created `Ingredient.cs` entity with name, category, unit, notes
   - Created `MealIngredient.cs` junction entity for many-to-many relationship
   - Updated `Meal.cs` to remove string-based ingredients
   - Enhanced `Household.cs` with ingredients navigation property

2. **Database Schema Updates**
   - Updated `HouseholdContext.cs` with new entities and relationships
   - Added foreign key constraints and cascade behaviors
   - Registered `IngredientService` in dependency injection

3. **Created IngredientService**
   - Full CRUD operations for ingredients
   - Search functionality with case-insensitive matching
   - Auto-creation of ingredients during meal entry
   - Category management and filtering

4. **Enhanced MealService**
   - Updated to work with `MealIngredient` relationships
   - Added methods for ingredient association/disassociation
   - Implemented ingredient-based meal querying
   - Maintained backward compatibility for existing features

5. **Sophisticated Meals UI**
   - **Ingredient Autocomplete** - Dynamic search with suggestions
   - **Duplicate Prevention** - Prevents adding same ingredient twice
   - **Rich Ingredient Display** - Shows quantities, units, notes
   - **Full Edit Capability** - Load existing ingredients, modify, save
   - **Reusable Components** - Shared ingredient form logic

### **Session 1 (June 22, 2025) - Initial Blazor Implementation**
1. **Converted Console App to Blazor Server**
   - Updated project file and dependencies
   - Created web host configuration
   - Implemented core UI infrastructure

2. **Created Essential Components**
   - Main layout, navigation, dashboard
   - Basic meal management (now superseded)
   - Family members and notes systems

---

## 🎨 UI/UX Design Decisions

### **Layout**
- **Sidebar Navigation:** Fixed left sidebar with collapsible mobile menu
- **Dashboard Cards:** Statistics overview with quick action buttons
- **Form Patterns:** Sophisticated ingredient forms with autocomplete
- **Responsive Design:** Mobile-first Bootstrap grid system

### **Meal Planning UX**
- **Weekly Calendar Grid:** Clean table layout with clear day/meal organization
- **Interactive Cells:** Click any cell to edit, visual feedback on hover
- **Inline Editing:** Edit in place with save/cancel options
- **Search Integration:** Autocomplete dropdown with meal suggestions
- **Contextual Previous Week:** Always visible for meal inspiration
- **Today Highlighting:** Yellow background for current day awareness
- **Navigation Clarity:** Clear Previous/Current/Next week controls

### **Ingredient System UX**
- **Autocomplete Dropdown:** Shows up to 5 suggestions with categories
- **Visual Feedback:** Selected ingredients displayed in cards
- **Quantity Management:** Decimal quantities with flexible units
- **One-Click Removal:** Easy ingredient deletion with × buttons
- **Form Persistence:** Maintains state during editing sessions

### **Color Scheme**
- **Primary:** Blue (#1b6ec2) for buttons and links
- **Meal Planning:** Yellow highlight for today, light backgrounds for data
- **Sidebar:** Dark gradient background
- **Cards:** Light gray borders with subtle shadows
- **Status Indicators:** Success (green), Warning (yellow), Danger (red)

---

## 🔧 Technical Implementation Notes

### **Meal Planning Architecture**
```csharp
// Weekly meal plan with automatic entry creation
public class MealPlan
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime WeekStartDate { get; set; }
    public int HouseholdId { get; set; }
    public ICollection<MealPlanEntry> Entries { get; set; } = new List<MealPlanEntry>();
}

// Individual meal assignments with flexibility
public class MealPlanEntry
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public MealTime MealTime { get; set; }
    public int? MealId { get; set; }           // Reference to existing meal
    public string? CustomMealName { get; set; }  // Or custom text
}
```

### **Advanced Blazor Patterns**
```csharp
// Dynamic week navigation with state management
private async Task LoadWeek(DateTime weekStart)
{
    currentMealPlan = await MealPlanService.CreateOrGetMealPlanAsync(householdId, weekStart);
    previousMealPlan = await MealPlanService.CreateOrGetMealPlanAsync(householdId, weekStart.AddDays(-7));
}

// Inline editing with meal suggestions
private void SearchMeals(string term)
{
    mealSuggestions = allMeals
        .Where(m => m.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
        .OrderBy(m => m.Name)
        .ToList();
}
```

### **Service Layer Integration**
```csharp
// Automatic meal plan creation with weekly structure
public async Task<MealPlan> CreateOrGetMealPlanAsync(int householdId, DateTime weekStartDate)
{
    var startOfWeek = GetStartOfWeek(weekStartDate);  // Sunday calculation
    // Creates 21 entries automatically (7 days × 3 meals)
}
```

---

## 🚀 Current Capabilities

### **Meal Planning Features**
1. **Weekly Structure** - Sunday through Saturday organization
2. **Three Meals Daily** - Breakfast, Lunch, Dinner for each day
3. **Flexible Assignment** - Use existing meals or custom text
4. **Week Navigation** - Easy movement between past, current, and future weeks
5. **Previous Week Reference** - Always shows last week for inspiration
6. **Today Awareness** - Highlights current day for quick reference
7. **Meal Integration** - Connects with existing meal library
8. **Auto-Cooking Tracking** - Marks meals as cooked when assigned to past dates

### **Ingredient Features**
1. **Smart Autocomplete** - Type to search existing ingredients
2. **Auto-Creation** - New ingredients created automatically
3. **Flexible Quantities** - Decimal amounts with custom units
4. **Rich Metadata** - Categories, default units, preparation notes
5. **Reusability** - Same ingredient across multiple meals
6. **Query Capabilities** - Find meals by ingredient

### **Meal Management**
1. **Complete CRUD** - Full create, read, update, delete operations
2. **Ingredient Integration** - Sophisticated ingredient selection
3. **Planning Integration** - Meals can be assigned to weekly plans
4. **Visual Enhancement** - Ingredient counts and detailed breakdowns
5. **Cooking Tracking** - Mark meals as cooked with timestamps

---

## 🐛 Known Issues & Limitations

### **Current Limitations**
1. **No Meal Plan Templates** - Cannot save/reuse common weekly patterns
2. **No Shopping List Generation** - Cannot auto-generate from meal plans yet
3. **Limited Batch Operations** - Cannot copy meals between weeks
4. **No Nutrition Tracking** - No nutritional analysis of planned meals
5. **Basic Mobile UX** - Table layout may need mobile optimization

### **Technical Debt**
1. **Testing** - No unit tests for meal planning system
2. **Performance** - Could optimize with pagination for large meal libraries
3. **Validation** - Limited client-side validation for meal assignments
4. **Error Handling** - Basic error handling, could be more robust
5. **Accessibility** - ARIA labels and keyboard navigation improvements needed

---

## 📋 Next Development Priorities

### **High Priority**
1. **Shopping List Generation** - Auto-generate grocery lists from meal plans
2. **Ingredient Aggregation** - Sum ingredients across week's planned meals
3. **Mobile Optimization** - Responsive meal planning interface for phones
4. **Meal Plan Templates** - Save and reuse common weekly patterns

### **Medium Priority**
1. **Bulk Operations** - Copy meals between weeks, duplicate plans
2. **Meal Plan History** - Archive and review past meal plans
3. **Recipe Integration** - Show ingredients when planning meals
4. **Nutrition Analysis** - Weekly nutrition summary from planned meals

### **Low Priority**
1. **Meal Plan Sharing** - Export/import meal plans
2. **Advanced Search** - Filter meals by dietary restrictions
3. **Cooking Reminders** - Notifications for planned meals
4. **Success Tracking** - Rate how well meal plans were followed

---

## 💡 Development Tips for Future Contributors

1. **Meal Planning Service** - Always use MealPlanService for plan operations
2. **Week Calculations** - Use GetStartOfWeek() helper for consistent Sunday starts
3. **UI State Management** - Maintain editing state properly in components
4. **Database Relationships** - Leverage existing meal/ingredient relationships
5. **Navigation Patterns** - Follow established patterns for week navigation
6. **Performance** - Use Include() statements for related data loading

---

## 📊 Current Database Status

- **Database File:** `household.db` (SQLite)
- **Schema Version:** Enhanced with meal planning system
- **Active Tables:** `MealPlans`, `MealPlanEntries` (in addition to previous)
- **Sample Data:** Created through interactive meal planning interface
- **Relationships:** Proper foreign keys with cascade behaviors
- **Weekly Structure:** Automatic creation of 21 entries per meal plan

---

*Last Updated: June 22, 2025*  
*Next Session: Implement shopping list generation from meal plans and ingredient aggregation*