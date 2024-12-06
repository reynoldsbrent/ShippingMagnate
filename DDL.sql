CREATE DATABASE shippingmagnate;
USE shippingmagnate;

-- Create SHIP table
CREATE TABLE SHIP (
    IMO_number CHAR(7) PRIMARY KEY,
    ship_name VARCHAR(100) NOT NULL,
    container_capacity INT NOT NULL
);

-- Create CONTAINER table
CREATE TABLE CONTAINER (
    BIC_code CHAR(11) PRIMARY KEY,
    size_TEU DECIMAL(6,2) NOT NULL,
    is_empty BOOLEAN NOT NULL,
    current_weight_kg DECIMAL(10,2) NOT NULL,
    load_capacity_kg DECIMAL(10,2) NOT NULL,
    total_volume_m3 DECIMAL(8,2) NOT NULL,
    used_volume_m3 DECIMAL(8,2) NOT NULL
);

-- Create PORT table
CREATE TABLE PORT (
    UN_code CHAR(5) PRIMARY KEY,
    port_name VARCHAR(100) NOT NULL,
    country VARCHAR(100) NOT NULL,
    latitude DECIMAL(9,6) NOT NULL,
    longitude DECIMAL(9,6) NOT NULL,
    docking_fee_usd DECIMAL(10,2) NOT NULL,
    mooring_fee_usd DECIMAL(10,2) NOT NULL,
    harbor_pilot_fee_usd DECIMAL(10,2) NOT NULL,
    tugboat_fee_usd DECIMAL(10,2) NOT NULL,
    terminal_handling_fee_usd DECIMAL(10,2) NOT NULL,
    agency_fee_usd DECIMAL(10,2) NOT NULL,
    inspection_fee_usd DECIMAL(10,2) NOT NULL
);

-- Create CUSTOMER table
CREATE TABLE CUSTOMER (
    customer_id CHAR(8) PRIMARY KEY,
    customer_name VARCHAR(100) NOT NULL,
    contact_name VARCHAR(100) NOT NULL,
    email VARCHAR(255) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    billing_address TEXT NOT NULL
);

-- Create SCHEDULE table
CREATE TABLE SCHEDULE (
    ship_IMO CHAR(7),
    arrival_port_code CHAR(5),
    departure_port_code CHAR(5) NOT NULL,
    arrival_date DATE,
    departure_date DATE NOT NULL,
    PRIMARY KEY (ship_IMO, arrival_port_code, arrival_date),
    FOREIGN KEY (ship_IMO) REFERENCES SHIP(IMO_number),
    FOREIGN KEY (arrival_port_code) REFERENCES PORT(UN_code),
    FOREIGN KEY (departure_port_code) REFERENCES PORT(UN_code)
);

-- Create TRANSPORTS table (M:N relationship between SHIP and CONTAINER)
CREATE TABLE TRANSPORTS (
    ship_IMO CHAR(7) NOT NULL,
    container_BIC CHAR(11) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE,
    PRIMARY KEY (ship_IMO, container_BIC, start_date),
    FOREIGN KEY (ship_IMO) REFERENCES SHIP(IMO_number),
    FOREIGN KEY (container_BIC) REFERENCES CONTAINER(BIC_code)
);

-- Create VISITS table (M:N relationship between SHIP and PORT)
CREATE TABLE VISITS (
    ship_IMO CHAR(7) NOT NULL,
    port_code CHAR(5) NOT NULL,
    visit_date DATE NOT NULL,
    PRIMARY KEY (ship_IMO, port_code, visit_date),
    FOREIGN KEY (ship_IMO) REFERENCES SHIP(IMO_number),
    FOREIGN KEY (port_code) REFERENCES PORT(UN_code)
);

-- Create HANDLES table (M:N relationship between PORT and CONTAINER)
CREATE TABLE HANDLES (
    port_code CHAR(5) NOT NULL,
    container_BIC CHAR(11) NOT NULL,
    handling_date DATE NOT NULL,
    PRIMARY KEY (port_code, container_BIC, handling_date),
    FOREIGN KEY (port_code) REFERENCES PORT(UN_code),
    FOREIGN KEY (container_BIC) REFERENCES CONTAINER(BIC_code)
);

-- Create SHIPS table (M:N relationship between CUSTOMER and CONTAINER)
CREATE TABLE SHIPS (
    customer_id CHAR(8) NOT NULL,
    container_BIC CHAR(11) NOT NULL,
    shipping_date DATE NOT NULL,
    PRIMARY KEY (customer_id, container_BIC, shipping_date),
    FOREIGN KEY (customer_id) REFERENCES CUSTOMER(customer_id),
    FOREIGN KEY (container_BIC) REFERENCES CONTAINER(BIC_code)
);