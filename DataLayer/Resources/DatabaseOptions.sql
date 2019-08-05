select d.name, compatibility_level,state_desc, user_access_desc, recovery_model_desc, collation_name, page_verify_option_desc,
                            is_read_only,
                            is_auto_close_on,
                            is_auto_shrink_on,
                            is_auto_create_stats_on,
                            is_auto_update_stats_on,
                            is_fulltext_enabled,
                            is_db_chaining_on,
                            is_trustworthy_on,
                            suser_sname(owner_sid) as Owner
                            from sys.databases d
                            where database_id > 4
                            order by d.name