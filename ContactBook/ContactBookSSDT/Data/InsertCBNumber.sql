if exists (select 1 from CB_contact where firstname = 'AfeenIdea')
begin
  insert into CB_Number(Number, ContactId, NumberTypeId) values('8767683308', 2, 1); 
end

if exists (select 1 from CB_contact where firstname = 'Tarik Idea')
begin
  insert into CB_Number(Number, ContactId, NumberTypeId) values('8652661233', 2, 1); 
end

if exists (select 1 from CB_contact where firstname = 'Tarik Idea')
begin
  insert into CB_Number(Number, ContactId, NumberTypeId) values('8652661233', 2, 1); 
end
