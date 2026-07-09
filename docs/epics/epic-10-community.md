# Epic 10 — Community

## Goal

Build a community-driven platform where users can contribute food products, improve catalog quality, earn reputation, and collaborate to maintain an accurate and comprehensive nutrition database.

## Scope

### Included

- User food submissions
- Community moderation
- Product approval workflow
- Reputation system
- Badges & achievements
- Product ratings
- Product reviews
- Contribution history
- Leaderboards
- Contributor profiles
- Public contributor statistics

### Excluded

- Authentication
- Food catalog browsing
- Food diary
- Administration

## Functional Requirements

| ID | Access | Category | Requirement |
|----|--------|----------|-------------|
| COMMUNITY-001 | User | Contributions | Create custom food products. |
| COMMUNITY-002 | User | Contributions | Edit own custom food products. |
| COMMUNITY-003 | User | Contributions | Delete own custom food products. |
| COMMUNITY-004 | User | Contributions | Submit custom food products for moderation. |
| COMMUNITY-005 | User | Contributions | View moderation status of submitted products. |
| COMMUNITY-006 | User | Contributions | Receive notifications when submissions are approved or rejected. |
| COMMUNITY-007 | User | Reviews | Rate public food products. |
| COMMUNITY-008 | User | Reviews | Write reviews for public food products. |
| COMMUNITY-009 | User | Reviews | Edit or delete own reviews. |
| COMMUNITY-010 | User | Reputation | Earn reputation points for approved contributions. |
| COMMUNITY-011 | User | Reputation | Lose reputation for rejected or removed contributions where applicable. |
| COMMUNITY-012 | User | Reputation | Unlock badges and achievements. |
| COMMUNITY-013 | User | Reputation | View contributor level and reputation. |
| COMMUNITY-014 | User | Profile | View personal contribution history. |
| COMMUNITY-015 | User | Profile | View contribution statistics. |
| COMMUNITY-016 | User | Leaderboards | Browse contributor leaderboards. |
| COMMUNITY-017 | User | Leaderboards | Rank contributors by reputation and approved submissions. |
| COMMUNITY-018 | Admin | Moderation | Review submitted food products. |
| COMMUNITY-019 | Admin | Moderation | Approve food products. |
| COMMUNITY-020 | Admin | Moderation | Reject food products with a reason. |
| COMMUNITY-021 | Admin | Moderation | Publish approved products to the global catalog. |
| COMMUNITY-022 | Admin | Moderation | Hide or remove inappropriate products. |

## User Stories

### User

- As a user, I want to create my own food products.
- As a user, I want to continue using my food products before moderation is completed.
- As a user, I want to submit useful products to the community.
- As a user, I want to know whether my submission has been approved.
- As a user, I want to improve my contributor reputation.
- As a user, I want to earn badges for helping the community.
- As a user, I want to compare my contributions with other users.
- As a user, I want to review products created by the community.

### Administrator

- As an administrator, I want to moderate user submissions.
- As an administrator, I want approved products to become publicly available.
- As an administrator, I want to maintain catalog quality.

## Technical Tasks

### Community

- [ ] Design contribution workflow
- [ ] Implement custom food products
- [ ] Implement submission workflow
- [ ] Implement moderation statuses
- [ ] Implement contributor profiles
- [ ] Implement contribution history

### Reviews

- [ ] Implement product ratings
- [ ] Implement product reviews
- [ ] Implement review management

### Reputation

- [ ] Design reputation model
- [ ] Implement reputation calculation
- [ ] Implement contributor levels
- [ ] Implement badges
- [ ] Implement achievements
- [ ] Implement leaderboards

### Moderation

- [ ] Implement moderation dashboard
- [ ] Implement approval workflow
- [ ] Implement rejection workflow
- [ ] Implement publication workflow

### Notifications

- [ ] Notify users when submissions are approved
- [ ] Notify users when submissions are rejected
- [ ] Notify users when badges are unlocked

### Quality

- [ ] Add integration tests
- [ ] Document Community API

## Acceptance Criteria

- Users can contribute food products.
- Custom food products remain available to their creators before moderation.
- Approved products become publicly available.
- Users can track submission status.
- Reputation is calculated automatically.
- Badges and achievements are awarded automatically.
- Leaderboards rank contributors fairly.
- Administrators can moderate submissions efficiently.

## Dependencies

- Authentication
- Food Catalog
- Food Diary
- Notifications
- Administration

## Status

Planned