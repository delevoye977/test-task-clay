CREATE OR REPLACE DATABASE `claydoors`;

USE `claydoors`;

CREATE TABLE `door` (
	`door_id` INT NOT NULL AUTO_INCREMENT COMMENT 'ID of the door',	
	`location` VARCHAR(50) NULL DEFAULT NULL COMMENT 'Location of the door',
	`description` VARCHAR(100) NULL DEFAULT NULL COMMENT 'Anything to note about this door',
	PRIMARY KEY (`door_id`)
)
COMMENT='Doors in the office'
;

INSERT INTO door (location, description) VALUES ('front door', 'door to access the building'), ('storage door', 'door of the storage room');


SELECT * FROM door;