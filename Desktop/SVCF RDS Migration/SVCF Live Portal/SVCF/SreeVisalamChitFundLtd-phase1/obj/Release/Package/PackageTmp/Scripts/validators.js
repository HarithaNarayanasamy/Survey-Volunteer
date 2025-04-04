

 ValidatorCommonOnSubmit = function() {                    

     ClearValidatorCallouts();

    var result = SetValidatorCallouts();                                                                                           

   return result;

  }

   

 ValidatorValidate = function(val, validationGroup, event) {

   val.isvalid = true;

     if ((typeof(val.enabled) == 'undefined' || val.enabled != false) && IsValidationGroupMatch(val, validationGroup)) {

        if (typeof(val.evaluationfunction) == 'function') {

            val.isvalid = val.evaluationfunction(val);

            if (!val.isvalid && Page_InvalidControlToBeFocused == null &&

                 typeof(val.focusOnError) == 'string' && val.focusOnError == 't') {

                 ValidatorSetFocus(val, event);

             }

        }

     }

      

      ClearValidatorCallouts();
      SetValidatorCallouts(); 



      ValidatorUpdateDisplay(val);

  }



 SetValidatorCallouts = function()

  {

      var i;

      var pageValid = true;                    
     for (i = 0; i < Page_Validators.length; i++) {         

       var inputControl = document.getElementById(Page_Validators[i].controltovalidate);               

      if (!Page_Validators[i].isvalid) {                                                        

         if(pageValid)

               inputControl.focus();

           WebForm_AppendToClassName(inputControl, 'error');

            pageValid = false;                                                     

        }                        

      }                    

    return pageValid;

  }

  

  ClearValidatorCallouts = function()

  {

    var i;                    

     var invalidConrols = [];

   for (i = 0; i < Page_Validators.length; i++) {         

         var inputControl = document.getElementById(Page_Validators[i].controltovalidate);               
          WebForm_RemoveClassName(inputControl, 'error');                                                  

      }                                        
  } 

