# Epic 06 — User Profile & Goals

## Goal

Allow users to manage their personal profile, body measurements, activity level, and nutrition goals. The system should calculate energy expenditure and recommended calorie and macronutrient targets based on the user's profile.

## Scope

### Included

- Personal profile
- Body measurements
- Activity level
- Nutrition goals
- Weight history
- BMR calculation
- TDEE calculation
- Daily calorie target
- Daily macronutrient targets
- Unit preferences

### Excluded

- Food diary
- Activity tracking
- Nutrition analytics
- Progress reports
- Health device integration

## Functional Requirements

| ID | Access | Category | Requirement |
|----|--------|----------|-------------|
| PROFILE-001 | User | Profile | View personal profile. |
| PROFILE-002 | User | Profile | Update body weight. |
| PROFILE-003 | User | Profile | Update height. |
| PROFILE-004 | User | Profile | Update date of birth. |
| PROFILE-005 | User | Profile | Update biological sex. |
| PROFILE-006 | User | Profile | Select activity level. |
| PROFILE-007 | User | Profile | Configure preferred measurement units (metric or imperial). |
| PROFILE-008 | User | Goals | Set a nutrition goal (lose, maintain, or gain weight). |
| PROFILE-009 | User | Goals | Set a target body weight. |
| PROFILE-010 | User | Goals | Configure the desired weekly weight change. |
| PROFILE-011 | User | Goals | Automatically calculate Basal Metabolic Rate (BMR). |
| PROFILE-012 | User | Goals | Automatically calculate Total Daily Energy Expenditure (TDEE). |
| PROFILE-013 | User | Goals | Calculate the recommended daily calorie target. |
| PROFILE-014 | User | Goals | Calculate recommended daily protein, fat, and carbohydrate targets. |
| PROFILE-015 | User | History | Record body weight history. |
| PROFILE-016 | User | History | Display historical body weight records. |
| PROFILE-017 | User | History | Edit body weight records. |
| PROFILE-018 | User | History | Delete body weight records. |

## User Stories

### User

- As a user, I want to maintain my personal profile.
- As a user, I want to record my body weight over time.
- As a user, I want to update my body measurements whenever they change.
- As a user, I want the application to calculate my BMR automatically.
- As a user, I want the application to estimate my TDEE.
- As a user, I want to choose whether I want to lose, maintain, or gain weight.
- As a user, I want the application to calculate my daily calorie target.
- As a user, I want the application to calculate my recommended macronutrient intake.
- As a user, I want to choose my preferred measurement units.

## Technical Tasks

### User Features

- [ ] Design the UserProfile domain model
- [ ] Design the WeightHistory domain model
- [ ] Implement profile CRUD
- [ ] Implement weight history CRUD
- [ ] Implement activity level management
- [ ] Implement nutrition goal management
- [ ] Implement BMR calculation
- [ ] Implement TDEE calculation
- [ ] Implement daily calorie target calculation
- [ ] Implement daily macronutrient calculation
- [ ] Implement unit preferences
- [ ] Add validation
- [ ] Add integration tests
- [ ] Document API

## Acceptance Criteria

- Users can manage their personal profile.
- Users can update body measurements.
- Users can configure nutrition goals.
- Users can record and manage body weight history.
- BMR is calculated automatically.
- TDEE is calculated automatically.
- Daily calorie targets are calculated automatically.
- Daily macronutrient targets are calculated automatically.
- Users can configure preferred measurement units.

## Dependencies

- Authentication

## Status

Planned