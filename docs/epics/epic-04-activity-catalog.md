# Epic 04 — Activity Catalog

## Goal

Provide users with a searchable catalog of physical activities to simplify activity logging and ensure accurate calorie expenditure estimation.

## Scope

### Included

- Activity catalog browsing
- Activity search
- Filtering
- Sorting
- Pagination
- Recently used activities
- Activity details page
- Activity management (Admin)
- Activity moderation (Admin)
- Activity import (Admin)

### Excluded

- Activity diary
- Activity tracking from wearable devices
- Workout plans

## Functional Requirements

| ID | Access | Category | Requirement |
|----|--------|----------|-------------|
| ACTIVITY-001 | User | Catalog | Browse the complete activity catalog. |
| ACTIVITY-002 | User | Catalog | Display activities using pagination. |
| ACTIVITY-003 | User | Catalog | Display essential activity information (name, category, MET value, estimated calories). |
| ACTIVITY-004 | User | Search | Search activities by name. |
| ACTIVITY-005 | User | Search | Support partial and case-insensitive search. |
| ACTIVITY-006 | User | Search | Return search results ordered by relevance. |
| ACTIVITY-007 | User | Filters | Filter activities by category. |
| ACTIVITY-008 | User | Filters | Filter activities by intensity level. |
| ACTIVITY-009 | User | Filters | Combine multiple filters in a single request. |
| ACTIVITY-010 | User | Sorting | Sort by relevance. |
| ACTIVITY-011 | User | Sorting | Sort alphabetically. |
| ACTIVITY-012 | User | Sorting | Sort by estimated calorie expenditure. |
| ACTIVITY-013 | User | Sorting | Sort in ascending or descending order. |
| ACTIVITY-014 | User | Recently Used | Display recently used activities. |
| ACTIVITY-015 | User | Recently Used | Order recently used activities by last usage date. |
| ACTIVITY-016 | User | Activity Details | Display complete activity information, including MET value, description, and calorie estimation guidance. |
| ACTIVITY-017 | Admin | Administration | Create activities. |
| ACTIVITY-018 | Admin | Administration | Update existing activities. |
| ACTIVITY-019 | Admin | Administration | Delete activities. |
| ACTIVITY-020 | Admin | Administration | Moderate activities before publication (if moderation is enabled). |
| ACTIVITY-021 | Admin | Administration | Import activities from external sources. |
| ACTIVITY-022 | Admin | Administration | Manage activity categories. |
| ACTIVITY-023 | Admin | Security | Only administrators can modify the activity catalog. |

## User Stories

### User

- As a user, I want to browse all available activities.
- As a user, I want to quickly find activities by name.
- As a user, I want to filter activities to narrow down search results.
- As a user, I want to sort activities by different criteria.
- As a user, I want to see activities I have recently used.
- As a user, I want to open an activity and view detailed information.

### Administrator

- As an administrator, I want to create activities.
- As an administrator, I want to edit activities.
- As an administrator, I want to delete activities.
- As an administrator, I want to moderate activities.
- As an administrator, I want to import activities from external sources.
- As an administrator, I want to manage activity categories.

## Technical Tasks

### User Features

- [ ] Design activity catalog query API
- [ ] Implement paginated activity listing
- [ ] Implement full-text search
- [ ] Implement filtering
- [ ] Implement sorting
- [ ] Implement recently used activities
- [ ] Implement activity details endpoint
- [ ] Add integration tests
- [ ] Document API

### Administrative Features

- [ ] Design activity management API
- [ ] Implement create activity endpoint
- [ ] Implement update activity endpoint
- [ ] Implement delete activity endpoint
- [ ] Implement activity moderation
- [ ] Implement activity import
- [ ] Implement bulk import
- [ ] Implement category management
- [ ] Add role-based authorization
- [ ] Add integration tests
- [ ] Document API

## Acceptance Criteria

- Users can browse the activity catalog.
- Users can search, filter and sort activities.
- Users can view detailed activity information.
- Recently used activities are displayed correctly.
- Only administrators can modify the activity catalog.
- Activities can be imported from external sources.
- Activities can be moderated before publication.

## Dependencies

- Authentication

## Status

Planned