# Epic 07 — Telegram Bot

## Goal

Provide a lightweight Telegram client that allows users to quickly track nutrition and physical activities, review daily progress, and access the most important features without opening the web application.

## Scope

### Included

- Authentication via Telegram
- Food diary management
- Activity diary management
- Daily nutrition summary
- Daily activity summary
- Recently used foods
- Recently used activities
- Favorites
- Quick search
- Barcode scanning
- Profile overview

### Excluded

- Administration
- Food catalog management
- Activity catalog management
- Analytics dashboards
- Advanced profile management

## Functional Requirements

| ID | Access | Category | Requirement |
|----|--------|----------|-------------|
| BOT-001 | User | Authentication | Sign in using Telegram. |
| BOT-002 | User | Dashboard | Display today's nutrition summary. |
| BOT-003 | User | Dashboard | Display today's activity summary. |
| BOT-004 | User | Dashboard | Display today's remaining calorie target. |
| BOT-005 | User | Food Diary | Add food entries. |
| BOT-006 | User | Food Diary | Edit food entries. |
| BOT-007 | User | Food Diary | Delete food entries. |
| BOT-008 | User | Food Diary | Browse diary entries for any selected date. |
| BOT-009 | User | Activity Diary | Add activity entries. |
| BOT-010 | User | Activity Diary | Edit activity entries. |
| BOT-011 | User | Activity Diary | Delete activity entries. |
| BOT-012 | User | Activity Diary | Browse activity entries for any selected date. |
| BOT-013 | User | Search | Search food products. |
| BOT-014 | User | Search | Search activities. |
| BOT-015 | User | Barcode | Search food products by barcode. |
| BOT-016 | User | Barcode | Recognize barcodes using the Telegram camera. |
| BOT-017 | User | Recently Used | Display recently used food products. |
| BOT-018 | User | Recently Used | Display recently used activities. |
| BOT-019 | User | Favorites | Display favorite foods and activities. |
| BOT-020 | User | Profile | Display current nutrition goals and profile summary. |
| BOT-021 | User | Synchronization | Synchronize all changes with the web application in real time. |

## User Stories

### User

- As a user, I want to quickly see how many calories I have consumed today.
- As a user, I want to quickly see how many calories I have burned today.
- As a user, I want to know how many calories I have left for today.
- As a user, I want to quickly add food entries without opening the web application.
- As a user, I want to quickly log physical activities.
- As a user, I want to search foods and activities directly from Telegram.
- As a user, I want to scan a barcode to quickly add a food product.
- As a user, I want to review my diary for any date.
- As a user, I want my Telegram bot and web application to stay synchronized.

## Technical Tasks

### Bot Features

- [ ] Create Telegram Bot
- [ ] Implement Telegram authentication
- [ ] Implement conversation/state management
- [ ] Implement today's dashboard
- [ ] Implement food diary commands
- [ ] Implement activity diary commands
- [ ] Implement food search
- [ ] Implement activity search
- [ ] Implement barcode scanning
- [ ] Implement recently used items
- [ ] Implement favorites
- [ ] Implement profile summary
- [ ] Implement date navigation
- [ ] Implement inline keyboards
- [ ] Implement synchronization with backend
- [ ] Add integration tests
- [ ] Document Bot API

## Acceptance Criteria

- Users can use the Telegram bot without visiting the web application for common daily tasks.
- Users can quickly add food and activity entries.
- Users can view nutrition and activity summaries.
- Users can search foods and activities.
- Users can scan food barcodes.
- All changes are synchronized with the web application.
- The bot provides a simplified user experience while sharing the same backend as the web application.

## Dependencies

- Authentication
- Food Catalog
- Food Diary
- Activity Catalog
- Activity Tracking
- User Profile & Goals

## Status

Planned