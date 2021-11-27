using Lary.Laboratory.Facebook.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     Object that controls news feed targeting for this post.Anyone in these groups will be more likely to see 
    ///     this post, others will be less likely, but may still see it anyway. Any of the targeting fields shown here 
    ///     can be used, none are required (applies to Pages only).
    /// </summary>
    public class FeedTargeting
    {
        /// <summary>
        ///     Maximum age.
        /// </summary>
        [FacebookProperty("age_max")]
        public int? AgeMax { get; set; }

        /// <summary>
        ///     Must be 13 or higher. Default is 0.
        /// </summary>
        [FacebookProperty("age_min")]
        public int AgeMin { get; set; } = 0;

        /// <summary>
        ///     Values of targeting cities. Use type of adcity to find Targeting Options and use the returned key to specify.
        /// </summary>
        [FacebookProperty("cities")]
        public List<int> Cities { get; set; }

        /// <summary>
        ///     Array of integers for graduation year from college.
        /// </summary>
        [FacebookProperty("college_years")]
        public List<int> CollegeYears { get; set; }

        /// <summary>
        ///     Values of targeting countries. You can specify up to 25 countries. Use ISO 3166 format codes.
        /// </summary>
        [FacebookProperty("countries")]
        public List<string> Countries { get; set; }

        /// <summary>
        ///     <para/>Array of integers for targeting based on education level. Use
        ///     <para/>1 for high school, 
        ///     <para/>2 for undergraduate, 
        ///     <para/>3 for alum(or localized equivalents).
        /// </summary>
        [FacebookProperty("education_statuses")]
        public List<int> EducationStatuses { get; set; }

        /// <summary>
        ///     <para/>arget specific genders. 
        ///     <para/>1 targets all male viewers and
        ///     <para/>2 females.Default is to target both.
        /// </summary>
        [FacebookProperty("genders")]
        public List<int> Genders { get; set; }

        /// <summary>
        ///     <para/>Indicates targeting based on the 'interested in' field of the user profile. You can specify 
        ///     an integer of 
        ///     <para/>1 to indicate male,
        ///     <para/>2 indicates female.Default is all types. Please note 'interested in' targeting is not available 
        ///     in France due to local laws.
        /// </summary>
        [FacebookProperty("interested_in")]
        public List<int> InterestedIn { get; set; }

        /// <summary>
        ///     One or more IDs of pages to target fans of pages. Use type of page to get possible IDs as Targeting 
        ///     Options and use the returned id to specify.
        /// </summary>
        [FacebookProperty("interests")]
        public List<int> Interests { get; set; }

        /// <summary>
        ///     Targeted locales. Use type of adlocale to find Targeting Options and use the returned key to specify.
        /// </summary>
        [FacebookProperty("locales")]
        public List<int> Locales { get; set; }

        /// <summary>
        ///     Values of targeting regions. Use type of adregion to find Targeting Options and use the returned key 
        ///     to specify.
        /// </summary>
        [FacebookProperty("regions")]
        public List<object> Regions { get; set; }

        /// <summary>
        ///     <para/>Array of integers for targeting based on relationship status. Use
        ///     <para/>1 for single,
        ///     <para/>2 for 'in a relationship',
        ///     <para/>3 for married, and
        ///     <para/>4 for engaged.Default is all types.
        /// </summary>
        [FacebookProperty("relationship_statuses")]
        public List<int> RelationshipStatuses { get; set; }
    }
}
