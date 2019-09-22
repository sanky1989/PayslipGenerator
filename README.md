# PayslipGenerator
This is .Net core MVC application containing front end, model, service, data access layer and testing modules.

#Assumption
This application accepts only csv input files with the format as below
first_name ,last_name,annual_salary,super_rate,payment_start_date
David,Rudd,60050,9%,01 March - 31 March
Ryan,Chen,120000,10%,01 April - 30 April

Annual Salary should be non negative number, super rate is followed by % and Payment start date are date range of complete months. 
Incomplete months will not be accepted.
