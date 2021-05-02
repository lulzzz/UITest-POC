
-- Remove Duplicated Activities (1606,1608,1917)
--انشطة الاستشارات الامنية
--الأنشطة الاستشارية الأخرى
--خدمات الموانئ

select * from LookUps.activity where ActivityId in (1607,1609,1919)
select * from LookUps.activity where ActivityId in (1606,1608,1917)

select * from LookUps.activity where ParentID in (1606,1608,1917)
select * from LookUps.activity where ParentID in (1607,1609,1919)

select * from Tender.TenderActivity where ActivityId in (1606,1608,1917)
select * from Tender.TenderActivity where ActivityId in (1607,1609,1919)

--First Step
update Tender.TenderActivity set ActivityId = 1607 where ActivityId = 1606
update Tender.TenderActivity set ActivityId = 1609 where ActivityId  = 1608
update Tender.TenderActivity set ActivityId = 1919 where ActivityId  = 1917

--Second Step
update LookUps.Activity set ParentID = 1607 where ParentID = 1606
update LookUps.Activity set ParentID = 1609 where ParentID = 1608
update LookUps.Activity set ParentID = 1919 where ParentID = 1917

-- Third Step 
delete LookUps.Activity where ActivityId in (1606,1608,1917)
