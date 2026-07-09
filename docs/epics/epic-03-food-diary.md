# Epic 03 — Food Diary

## Goal

Provide users with a personal food diary to record daily food consumption, monitor nutritional intake, and review historical data.

## Scope

### Included

- Food entry management
- Meal organization
- Daily diary
- Historical diary
- Daily nutrition summary
- Portion management
- Favorite meals
- Recently used products
- Personal food products
- Personal food product moderation

### Excluded

- Food catalog management
- Activity tracking
- Nutrition reports

## Functional Requirements

| ID | Access | Category | Requirement |
|----|--------|----------|-------------|
| DIARY-001 | User | Food Entries | Add a food entry to the diary. |
| DIARY-002 | User | Food Entries | Edit a food entry. |
| DIARY-003 | User | Food Entries | Delete a food entry. |
| DIARY-004 | User | Food Entries | Assign a meal type (Breakfast, Lunch, Dinner, Snack). |
| DIARY-005 | User | Food Entries | Select the diary date for a food entry. |
| DIARY-006 | User | Food Entries | Store diary entries independently of the user's current time zone. |
| DIARY-007 | User | Food Entries | Update serving size and quantity. |
| DIARY-008 | User | Food Entries | Support different serving units. |
| DIARY-009 | User | Food Entries | Calculate nutritional values based on serving size. |
| DIARY-010 | User | Diary | View the diary for any selected date. |
| DIARY-011 | User | Diary | Navigate between previous and next dates. |
| DIARY-012 | User | Diary | Display food entries grouped by meal type. |
| DIARY-013 | User | Diary | Display a daily nutrition summary for the selected date. |
| DIARY-014 | User | Diary | Display calories, protein, fat and carbohydrates for the selected date. |
| DIARY-015 | User | Diary | Display the remaining daily calorie allowance. |
| DIARY-016 | User | History | Browse diary history by date. |
| DIARY-017 | User | History | Copy food entries from another date. |
| DIARY-018 | User | Recently Used | Display recently used food products. |
| DIARY-019 | User | Favorites | Save frequently used meals as favorites. |
| DIARY-020 | User | Favorites | Add meals from favorites. |
| DIARY-021 | User | Personal Products | Create a personal food product. |
| DIARY-022 | User | Personal Products | Edit personal food products. |
| DIARY-023 | User | Personal Products | Delete personal food products. |
| DIARY-024 | User | Personal Products | View all personal food products. |
| DIARY-025 | User | Personal Products | Use personal food products in food entries. |
| DIARY-026 | User | Personal Products | Display personal food products in recently used products. |
| DIARY-027 | User | Personal Products | Submit personal food products for moderation. |
| DIARY-028 | Admin | Moderation | Review submitted personal food products. |
| DIARY-029 | Admin | Moderation | Approve submitted food products and publish them to the global catalog. |
| DIARY-030 | Admin | Moderation | Reject submitted food products with an optional reason. |

## User Stories

### User

- As a user, I want to add food entries to my diary.
- As a user, I want to edit or delete food entries.
- As a user, I want to change portion sizes and serving units.
- As a user, I want to assign food entries to breakfast, lunch, dinner or snacks.
- As a user, I want to view my diary for any date.
- As a user, I want to navigate between different days.
- As a user, I want to copy food entries from another day.
- As a user, I want to see my daily calories and macronutrients.
- As a user, I want to quickly access recently used food products.
- As a user, I want to save frequently used meals as favorites.
- As a user, I want to create my own food products when they are not available in the catalog.
- As a user, I want to manage my personal food products.
- As a user, I want to submit my personal food products for review so they can become available to everyone.

### Administrator

- As an administrator, I want to review submitted food products.
- As an administrator, I want to approve high-quality food products.
- As an administrator, I want to reject invalid or duplicate food products.

## Technical Tasks

### User Features

- [ ] Design the FoodEntry domain model
- [ ] Implement food entry CRUD
- [ ] Implement meal grouping by meal type
- [ ] Implement serving size calculation
- [ ] Implement nutrition calculations
- [ ] Implement diary navigation by date
- [ ] Implement daily nutrition summary
- [ ] Implement recently used products
- [ ] Implement favorite meals
- [ ] Implement personal food products
- [ ] Implement submission for moderation
- [ ] Add integration tests
- [ ] Document API

### Administrative Features

- [ ] Implement moderation endpoints
- [ ] Implement approve/reject workflow
- [ ] Add role-based authorization
- [ ] Add integration tests
- [ ] Document API

## Acceptance Criteria

- Users can create, edit and delete food entries.
- Users can adjust serving sizes and quantities.
- Nutritional values are calculated automatically.
- Users can view their diary for any selected date.
- Food entries remain associated with the selected diary date regardless of time zone changes.
- Recently used products are displayed correctly.
- Favorite meals can be reused.
- Users can create and manage personal food products.
- Personal food products can be submitted for moderation.
- Approved food products become available in the global food catalog.

## Dependencies

- Authentication
- Food Catalog

## Status

Planned