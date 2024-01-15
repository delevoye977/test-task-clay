CREATE OR REPLACE DATABASE `claydoors`;

USE `claydoors`;

CREATE TABLE `door` (
	`door_id` INT UNSIGNED NOT NULL AUTO_INCREMENT COMMENT 'ID of the door',	
	`location` VARCHAR(50) NULL DEFAULT NULL COMMENT 'Location of the door',
	`description` VARCHAR(100) NULL DEFAULT NULL COMMENT 'Anything to note about this door',
	PRIMARY KEY (`door_id`)
)
COMMENT='Doors in the office'
;

CREATE TABLE `door_user` (
	`user_id` INT UNSIGNED NOT NULL AUTO_INCREMENT COMMENT 'ID of the user',
	`username` VARCHAR(100) NULL DEFAULT NULL,
	PRIMARY KEY (`user_id`)
)
COMMENT='Users of the doors'
;


INSERT INTO door (location, description) VALUES ('front door', 'door to access the building'), ('storage door', 'door of the storage room');
INSERT INTO door_user (username) VALUES ('Big boss'), ('Floor boss'), ('Standard employee'), ('Intern employee'), ('Senior employee');