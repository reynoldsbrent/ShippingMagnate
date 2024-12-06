USE shippingmagnate;

-- Get total number of empty containers
DELIMITER $$
CREATE PROCEDURE GetEmptyContainerCount(
    OUT emptyCount INT
)
BEGIN
    SELECT COUNT(*) INTO emptyCount
    FROM CONTAINER
    WHERE is_empty = TRUE;
END $$
DELIMITER ;

-- Update container status
DELIMITER $$
CREATE PROCEDURE UpdateContainerStatus(
    IN containerBIC CHAR(11),
    IN isEmpty BOOLEAN
)
BEGIN
    UPDATE CONTAINER
    SET is_empty = isEmpty
    WHERE BIC_code = containerBIC;
END $$
DELIMITER ;

-- Get ships currently at port
DELIMITER $$
CREATE PROCEDURE GetShipsAtPort(
    IN portCode CHAR(5)
)
BEGIN
    SELECT s.ship_name, s.IMO_number, v.visit_date
    FROM SHIP s
    JOIN VISITS v ON s.IMO_number = v.ship_IMO
    WHERE v.port_code = portCode
    AND v.visit_date = CURDATE();
END $$
DELIMITER ;

-- Get customer container count
DELIMITER $$
CREATE PROCEDURE GetCustomerContainerCount(
    IN customerId CHAR(8),
    OUT containerCount INT
)
BEGIN
    SELECT COUNT(DISTINCT container_BIC) INTO containerCount
    FROM SHIPS
    WHERE customer_id = customerId;
END $$
DELIMITER ;

-- List containers at port
DELIMITER $$
CREATE PROCEDURE ListContainersAtPort(
    IN portCode CHAR(5)
)
BEGIN
    SELECT 
        c.BIC_code,
        c.size_TEU,
        c.is_empty,
        h.handling_date
    FROM CONTAINER c
    JOIN HANDLES h ON c.BIC_code = h.container_BIC
    WHERE h.port_code = portCode
    ORDER BY h.handling_date DESC;
END $$
DELIMITER ;