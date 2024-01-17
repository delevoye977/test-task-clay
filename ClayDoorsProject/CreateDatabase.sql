CREATE OR REPLACE DATABASE `claydoors`;

USE `claydoors`;

CREATE TABLE `door` (
	`door_id` INT NOT NULL AUTO_INCREMENT,	
	`location` VARCHAR(50) NULL DEFAULT NULL,
	`description` VARCHAR(100) NULL DEFAULT NULL,
	PRIMARY KEY (`door_id`)
)
COMMENT='Doors in the office'
;

CREATE TABLE `door_user` (
	`user_id` INT NOT NULL AUTO_INCREMENT,
	`username` VARCHAR(100) NULL DEFAULT NULL,
	PRIMARY KEY (`user_id`)
)
COMMENT='Users of the doors'
;

CREATE TABLE `role` (
	`role_id` INT NOT NULL AUTO_INCREMENT,
	`name` VARCHAR(50) NOT NULL DEFAULT '',
	`description` VARCHAR(100) NULL DEFAULT NULL,
	PRIMARY KEY (`role_id`)
)
COMMENT='Roles that users can have'
;

CREATE TABLE `permission` (
	`permission_id` INT NOT NULL AUTO_INCREMENT,
	`name` VARCHAR(50) NOT NULL DEFAULT '',
	`description` VARCHAR(100) NULL DEFAULT NULL,
	PRIMARY KEY (`permission_id`)
)
COMMENT='Permissions that roles can have or that are required'
;

CREATE TABLE `user_role_link` (
	`user_id` INT NOT NULL,
	`role_id` INT NOT NULL,
	PRIMARY KEY (`user_id`, `role_id`),
	CONSTRAINT `fk_door_user_user_role_link` FOREIGN KEY (`user_id`) REFERENCES `door_user` (`user_id`) ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT `fk_role_user_role_link` FOREIGN KEY (`role_id`) REFERENCES `role` (`role_id`) ON UPDATE NO ACTION ON DELETE NO ACTION
)
COMMENT='List of roles a user has'
;

CREATE TABLE `role_permission_link` (
	`role_id` INT NOT NULL,
	`permission_id` INT NOT NULL,
	PRIMARY KEY (`role_id`, `permission_id`),
	CONSTRAINT `fk_role_role_permission_link` FOREIGN KEY (`role_id`) REFERENCES `role` (`role_id`) ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT `fk_permission_role_permission_link` FOREIGN KEY (`permission_id`) REFERENCES `permission` (`permission_id`) ON UPDATE NO ACTION ON DELETE NO ACTION
)
COMMENT='List of permissions a role has'
;

CREATE TABLE `door_permission_link` (
	`door_id` INT NOT NULL,
	`permission_id` INT NOT NULL,
	PRIMARY KEY (`door_id`, `permission_id`),
	CONSTRAINT `fk_door_door_permission_link` FOREIGN KEY (`door_id`) REFERENCES `door` (`door_id`) ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT `fk_permission_door_permission_link` FOREIGN KEY (`permission_id`) REFERENCES `permission` (`permission_id`) ON UPDATE NO ACTION ON DELETE NO ACTION
)
COMMENT='List of required permissions to unlock a door'
;

CREATE TABLE `door_unlock_log` (
	`log_id` INT NOT NULL AUTO_INCREMENT,
	`action_time` TIMESTAMP NOT NULL,
	`action_result` VARCHAR(15) NOT NULL,
	`username` VARCHAR(100) NULL DEFAULT NULL,
	`door_id` INT NULL DEFAULT NULL,
	PRIMARY KEY (`log_id`)
)
COMMENT ='Logs of attempts to unlock a door'
;

INSERT INTO door (location, description) VALUES ('Front door', 'door to access the building'), ('Storage door', 'door of the storage room');
INSERT INTO door_user (username) VALUES ('Big boss'), ('Floor boss'), ('Standard employee'), ('Intern employee'), ('Senior employee');
INSERT INTO role (NAME, DESCRIPTION) VALUES ('Director', 'Director of the company'), ('Office manager', 'Manager of the office'), ('Employee', 'Employee in the company');
INSERT INTO permission (NAME, DESCRIPTION) VALUES ('Building access', 'Grants access to the front door of the buiding'), ('Storage access', 'Grants access to the storage room');

INSERT INTO door_permission_link (door_id, permission_id) VALUES 
	((SELECT door_id FROM door WHERE location = 'Front door'), (SELECT permission_id FROM permission WHERE NAME = 'Building access')),
	((SELECT door_id FROM door WHERE location = 'Storage door'), (SELECT permission_id FROM permission WHERE NAME = 'Storage access'));
	
INSERT INTO role_permission_link (role_id, permission_id) VALUES
	((SELECT role_id FROM role WHERE NAME = 'Director'), (SELECT permission_id FROM permission WHERE NAME = 'Building access')),
	((SELECT role_id FROM role WHERE NAME = 'Director'), (SELECT permission_id FROM permission WHERE NAME = 'Storage access')),
	((SELECT role_id FROM role WHERE NAME = 'Office manager'), (SELECT permission_id FROM permission WHERE NAME = 'Storage access')),
	((SELECT role_id FROM role WHERE NAME = 'Employee'), (SELECT permission_id FROM permission WHERE NAME = 'Building access'));

INSERT INTO user_role_link VALUES
	((SELECT user_id FROM door_user WHERE username = 'Big boss'), (SELECT role_id FROM role WHERE NAME = 'Director')),
	((SELECT user_id FROM door_user WHERE username = 'Floor boss'), (SELECT role_id FROM role WHERE NAME = 'Office manager')),
	((SELECT user_id FROM door_user WHERE username = 'Floor boss'), (SELECT role_id FROM role WHERE NAME = 'Employee')),
	((SELECT user_id FROM door_user WHERE username = 'Standard employee'), (SELECT role_id FROM role WHERE NAME = 'Employee')),
	((SELECT user_id FROM door_user WHERE username = 'Intern employee'), (SELECT role_id FROM role WHERE NAME = 'Employee')),
	((SELECT user_id FROM door_user WHERE username = 'Senior employee'), (SELECT role_id FROM role WHERE NAME = 'Employee'));

SELECT * FROM door_user;
SELECT * FROM door;
SELECT * FROM door_permission_link;