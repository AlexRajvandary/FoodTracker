# Epic 05 — Activity Tracking

## Goal

Provide users with a personal activity diary to record physical activities, track calorie expenditure, and review activity history.

## Scope

### Included

- Activity entry management
- Activity organization
- Daily activity diary
- Historical activity diary
- Daily calorie expenditure summary
- Duration and distance tracking
- Favorite activities
- Recently used activities
- Personal activities
- Personal activity moderation

### Excluded

- Activity catalog management
- Workout plans
- Wearable device integration
- Health platform synchronization

## Functional Requirements

| ID | Access | Category | Requirement |
|----|--------|----------|-------------|
| TRACK-001 | User | Activity Entries | Add an activity entry to the diary. |
| TRACK-002 | User | Activity Entries | Edit an activity entry. |
| TRACK-003 | User | Activity Entries | Delete an activity entry. |
| TRACK-004 | User | Activity Entries | Select the diary date for an activity entry. |
| TRACK-005 | User | Activity Entries | Store activity entries independently of the user's current time zone. |
| TRACK-006 | User | Activity Entries | Specify activity duration. |
| TRACK-007 | User | Activity Entries | Specify activity distance when applicable. |
| TRACK-008 | User | Activity Entries | Calculate calories burned based on activity data and user profile. |
| TRACK-009 | User | Diary | View the activity diary for any selected date. |
| TRACK-010 | User | Diary | Navigate between previous and next dates. |
| TRACK-011 | User | Diary | Display all activity entries for the selected date. |
| TRACK-012 | User | Diary | Display total calories burned for the selected date. |
| TRACK-013 | User | Diary | Display total activity duration for the selected date. |
| TRACK-014 | User | History | Browse activity history by date. |
| TRACK-015 | User | History | Copy activity entries from another date. |
| TRACK-016 | User | Recently Used | Display recently used activities. |
| TRACK-017 | User | Favorites | Save frequently used activities as favorites. |
| TRACK-018 | User | Favorites | Add activity entries from favorites. |
| TRACK-019 | User | Personal Activities | Create a personal activity when it does not exist in the catalog. |
| TRACK-020 | User | Personal Activities | Edit personal activities. |
| TRACK-021 | User | Personal Activities | Delete personal activities. |
| TRACK-022 | User | Personal Activities | View all personal activities. |
| TRACK-023 | User | Personal Activities | Use personal activities in activity entries. |
| TRACK-024 | User | Personal Activities | Display personal activities in recently used activities. |
| TRACK-025 | User | Personal Activities | Submit personal activities for moderation. |
| TRACK-026 | Admin | Moderation | Review submitted personal activities. |
| TRACK-027 | Admin | Moderation | Approve submitted activities and publish them to the global catalog. |
| TRACK-028 | Admin | Moderation | Reject submitted activities with an optional reason. |

## User Stories

### User

- As a user, I want to add activity entries to my diary.
- As a user, I want to edit or delete activity entries.
- As a user, I want to specify activity duration and distance.
- As a user, I want to view my activity diary for any date.
- As a user, I want to navigate between different days.
- As a user, I want to copy activity entries from another day.
- As a user, I want to see my daily calories burned.
- As a user, I want to quickly access recently used activities.
- As a user, I want to save frequently used activities as favorites.
- As a user, I want to create my own activities when they are not available in the catalog.
- As a user, I want to manage my personal activities.
- As a user, I want to submit my personal activities for review so they can become available to everyone.

### Administrator

- As an administrator, I want to review submitted activities.
- As an administrator, I want to approve high-quality activities.
- As an administrator, I want to reject invalid or duplicate activities.

## Technical Tasks

### User Features

- [ ] Design the ActivityEntry domain model
- [ ] Implement activity entry CRUD
- [ ] Implement calorie calculation
- [ ] Implement duration and distance tracking
- [ ] Implement diary navigation by date
- [ ] Implement daily activity summary
- [ ] Implement recently used activities
- [ ] Implement favorite activities
- [ ] Implement personal activities
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

- Users can create, edit and delete activity entries.
- Calories burned are calculated automatically.
- Users can view their activity diary for any selected date.
- Activity entries remain associated with the selected diary date regardless of time zone changes.
- Recently used activities are displayed correctly.
- Favorite activities can be reused.
- Users can create and manage personal activities.
- Personal activities can be submitted for moderation.
- Approved activities become available in the global activity catalog.

## Dependencies

- Authentication
- Activity Catalog

## Status

Planned