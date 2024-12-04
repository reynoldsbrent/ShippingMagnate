USE shippingmagnate;
-- Insert Ships
INSERT INTO SHIP VALUES
('9123456', 'Pacific Pioneer', 5000),
('9234567', 'Atlantic Voyager', 4500),
('9345678', 'Nordic Explorer', 6000),
('9456789', 'Mediterranean Spirit', 3500),
('9567890', 'Asian Enterprise', 4800),
('9678901', 'Arctic Navigator', 5200),
('9789012', 'Southern Star', 4000),
('9890123', 'Caribbean Clipper', 3800),
('9901234', 'Indian Ocean Express', 4200),
('9012345', 'Global Frontier', 5500);

-- Insert Containers
INSERT INTO CONTAINER VALUES
('MSCU1234567', 1.0, FALSE, 15000.50, 21000.00, 33.2, 28.5),
('APLU2345678', 2.0, TRUE, 0.00, 30000.00, 67.3, 0.0),
('CMAU3456789', 1.0, FALSE, 18500.75, 20000.00, 33.2, 30.1),
('EGHU4567890', 2.0, FALSE, 25000.00, 32000.00, 67.3, 55.4),
('TCNU5678901', 1.0, TRUE, 0.00, 21500.00, 33.2, 0.0),
('NYKU6789012', 1.0, FALSE, 19800.25, 22000.00, 33.2, 29.8),
('SUDU7890123', 2.0, FALSE, 27500.50, 31000.00, 67.3, 60.2),
('HLXU8901234', 1.0, TRUE, 0.00, 20500.00, 33.2, 0.0),
('UACU9012345', 1.0, FALSE, 17800.75, 21000.00, 33.2, 27.9),
('OCLU0123456', 2.0, FALSE, 28500.25, 32000.00, 67.3, 58.7);

-- Insert Ports
INSERT INTO PORT VALUES
('USLAX', 'Port of Los Angeles', 'United States', 33.7288, -118.2620, 1500.00, 800.00, 600.00, 900.00, 1200.00, 400.00, 300.00),
('CNSHA', 'Port of Shanghai', 'China', 31.2304, 121.4737, 1200.00, 700.00, 500.00, 800.00, 1000.00, 350.00, 250.00),
('SGSIN', 'Port of Singapore', 'Singapore', 1.2749, 103.8087, 1400.00, 750.00, 550.00, 850.00, 1100.00, 375.00, 275.00),
('NLRTM', 'Port of Rotterdam', 'Netherlands', 51.9225, 4.4792, 1600.00, 850.00, 650.00, 950.00, 1300.00, 425.00, 325.00),
('HKHKG', 'Port of Hong Kong', 'Hong Kong', 22.2938, 114.1687, 1300.00, 725.00, 525.00, 825.00, 1050.00, 362.50, 262.50),
('AEDXB', 'Port of Dubai', 'UAE', 25.2767, 55.2920, 1450.00, 775.00, 575.00, 875.00, 1150.00, 387.50, 287.50),
('DEHAM', 'Port of Hamburg', 'Germany', 53.5511, 9.9937, 1550.00, 825.00, 625.00, 925.00, 1250.00, 412.50, 312.50),
('JPYOK', 'Port of Yokohama', 'Japan', 35.4437, 139.6380, 1350.00, 737.50, 537.50, 837.50, 1075.00, 368.75, 268.75),
('GBFXT', 'Port of Felixstowe', 'United Kingdom', 51.9619, 1.3510, 1475.00, 787.50, 587.50, 887.50, 1175.00, 393.75, 293.75),
('KRPUS', 'Port of Busan', 'South Korea', 35.1796, 129.0756, 1325.00, 712.50, 512.50, 812.50, 1025.00, 356.25, 256.25);

-- Insert Customers
INSERT INTO CUSTOMER VALUES
('CUST0001', 'Global Trading Co', 'John Smith', 'john.smith@globaltrading.com', '+1-555-0123', '123 Business Ave, New York, NY 10001, USA'),
('CUST0002', 'Euro Logistics Ltd', 'Marie Dubois', 'marie.dubois@eurologistics.eu', '+33-555-0124', '45 Commerce St, Paris, 75001, France'),
('CUST0003', 'Asian Imports Inc', 'Lee Wei', 'lee.wei@asianimports.com', '+65-555-0125', '78 Trade Road, Singapore, 123456'),
('CUST0004', 'Pacific Express', 'James Wilson', 'james.wilson@pacificexpress.com', '+1-555-0126', '456 Shipping Lane, Los Angeles, CA 90001, USA'),
('CUST0005', 'Nordic Freight AS', 'Erik Hansen', 'erik.hansen@nordicfreight.no', '+47-555-0127', '89 Harbor St, Oslo, 0001, Norway'),
('CUST0006', 'Mediterranean Shipping', 'Sofia Costa', 'sofia.costa@medshipping.es', '+34-555-0128', '12 Port Avenue, Barcelona, 08001, Spain'),
('CUST0007', 'Australian Cargo Ltd', 'Tom Brown', 'tom.brown@auscargo.com.au', '+61-555-0129', '34 Docker Road, Sydney, NSW 2000, Australia'),
('CUST0008', 'Americas United Log', 'Carlos Santos', 'carlos.santos@amunited.com', '+55-555-0130', '67 Logistics Way, Sao Paulo, 01000-000, Brazil'),
('CUST0009', 'Middle East Trade', 'Ahmed Hassan', 'ahmed.hassan@metrade.ae', '+971-555-0131', '90 Business Bay, Dubai, UAE'),
('CUST0010', 'African Logistics', 'Sarah Mbeki', 'sarah.mbeki@afrilog.za', '+27-555-0132', '123 Harbor Road, Cape Town, 8001, South Africa');

-- Insert Schedules (Current and future dates)
INSERT INTO SCHEDULE VALUES
('9123456', 'USLAX', 'CNSHA', '2024-12-15', '2024-12-01'),
('9234567', 'SGSIN', 'NLRTM', '2024-12-20', '2024-12-05'),
('9345678', 'HKHKG', 'AEDXB', '2024-12-25', '2024-12-10'),
('9456789', 'DEHAM', 'JPYOK', '2024-12-30', '2024-12-15'),
('9567890', 'GBFXT', 'KRPUS', '2025-01-05', '2024-12-20'),
('9678901', 'USLAX', 'SGSIN', '2025-01-10', '2024-12-25'),
('9789012', 'CNSHA', 'HKHKG', '2025-01-15', '2024-12-30'),
('9890123', 'NLRTM', 'DEHAM', '2025-01-20', '2025-01-05'),
('9901234', 'AEDXB', 'GBFXT', '2025-01-25', '2025-01-10'),
('9012345', 'JPYOK', 'KRPUS', '2025-01-30', '2025-01-15');

-- Insert Transport Records
INSERT INTO TRANSPORTS VALUES
('9123456', 'MSCU1234567', '2024-12-01', NULL),
('9234567', 'APLU2345678', '2024-12-05', '2024-12-20'),
('9345678', 'CMAU3456789', '2024-12-10', NULL),
('9456789', 'EGHU4567890', '2024-12-15', NULL),
('9567890', 'TCNU5678901', '2024-12-20', '2025-01-05'),
('9678901', 'NYKU6789012', '2024-12-25', NULL),
('9789012', 'SUDU7890123', '2024-12-30', NULL),
('9890123', 'HLXU8901234', '2025-01-05', '2025-01-20'),
('9901234', 'UACU9012345', '2025-01-10', NULL),
('9012345', 'OCLU0123456', '2025-01-15', NULL);

-- Insert Visit Records
INSERT INTO VISITS VALUES
('9123456', 'USLAX', '2024-12-01'),
('9234567', 'SGSIN', '2024-12-05'),
('9345678', 'HKHKG', '2024-12-10'),
('9456789', 'DEHAM', '2024-12-15'),
('9567890', 'GBFXT', '2024-12-20'),
('9678901', 'USLAX', '2024-12-25'),
('9789012', 'CNSHA', '2024-12-30'),
('9890123', 'NLRTM', '2025-01-05'),
('9901234', 'AEDXB', '2025-01-10'),
('9012345', 'JPYOK', '2025-01-15');

-- Insert Handling Records
INSERT INTO HANDLES VALUES
('USLAX', 'MSCU1234567', '2024-12-01'),
('SGSIN', 'APLU2345678', '2024-12-05'),
('HKHKG', 'CMAU3456789', '2024-12-10'),
('DEHAM', 'EGHU4567890', '2024-12-15'),
('GBFXT', 'TCNU5678901', '2024-12-20'),
('USLAX', 'NYKU6789012', '2024-12-25'),
('CNSHA', 'SUDU7890123', '2024-12-30'),
('NLRTM', 'HLXU8901234', '2025-01-05'),
('AEDXB', 'UACU9012345', '2025-01-10'),
('JPYOK', 'OCLU0123456', '2025-01-15');

-- Insert Shipping Records
INSERT INTO SHIPS VALUES
('CUST0001', 'MSCU1234567', '2024-12-01'),
('CUST0002', 'APLU2345678', '2024-12-05'),
('CUST0003', 'CMAU3456789', '2024-12-10'),
('CUST0004', 'EGHU4567890', '2024-12-15'),
('CUST0005', 'TCNU5678901', '2024-12-20'),
('CUST0006', 'NYKU6789012', '2024-12-25'),
('CUST0007', 'SUDU7890123', '2024-12-30'),
('CUST0008', 'HLXU8901234', '2025-01-05'),
('CUST0009', 'UACU9012345', '2025-01-10'),
('CUST0010', 'OCLU0123456', '2025-01-15');