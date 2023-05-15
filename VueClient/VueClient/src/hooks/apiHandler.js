import axios from 'axios'

   // without axios
    // export const getData = async function(url){
    //     const response = await fetch(url);
    //     if(!response.ok)
    //         errorHandler(response);
    //     return await response.json();
    //     };
 
    // with axios
        export const getData = async function(url){
          const response = await axios.get(url);
          if(response.status != 200)
              errorHandler(response);
          return response.data;
          };
    
    export const postData = async function(url, body){
      const response = await fetch(url,{
        method: 'POST',
        headers: {"Content-type":'application/json'},
        body : JSON.stringify(body)
      });
     if(!response.ok)
      errorHandler(response);
    }
    
    export const deleteData = async function(url){
      const response = await fetch(url, {method: 'DELETE'});
      if(!response.ok)
      errorHandler(response);
    }

    function errorHandler(response){
        const error = new Error(response.message || 'Failed to fetch');
        throw error;
    }

