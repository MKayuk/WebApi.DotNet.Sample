using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scansource.Shared.Notification.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DotNet.Sample.DTO;
using WebApi.DotNet.Sample.Services.Interface;

namespace WebApi.DotNet.Sample.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(INotificationHandler<MyNotification> notifications, ICustomerService customerService)
            : base(notifications)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Insert a new customer
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] CustomerDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Notify();

                return Ok(await _customerService.Add(dto));
            }
            catch (Exception ex)
            {
                return Problem(ex.StackTrace, title: ex.Message);
            }
        }

        /// <summary>
        /// Return all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CustomerDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _customerService.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.StackTrace, title: ex.Message);
            }
        }

        /// <summary>
        /// Get a customer by document
        /// </summary>
        /// <param name="document" >Only numbers</param>
        /// <returns></returns>
        [HttpGet]
        [Route("document/{document}")]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(int document)
        {
            try
            {
                if (document.ToString().Length != 11 || document.ToString().Length != 14)
                    return BadRequest("Document is invalid or missing.");

                var result = await _customerService
                    .GetCustomerByDocument(document);

                return Execute(() =>
                {
                    if (result is null)
                        return NotFound();

                    return Ok(result);
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.StackTrace, title: ex.Message);
            }
        }

        /// <summary>
        /// Update a customer
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put([FromBody] CustomerDto dto, string id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Notify();

                var result = await _customerService.Update(dto, id);

                return Execute(() =>
                {
                    if (result)
                        return NoContent();

                    return BadRequest();
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.StackTrace, title: ex.Message);
            }
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Notify();

                var result = await _customerService.Delete(id);

                return Execute(() =>
                {
                    if (result)
                        return NoContent();

                    return BadRequest();
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.StackTrace, title: ex.Message);
            }
        }
    }
}