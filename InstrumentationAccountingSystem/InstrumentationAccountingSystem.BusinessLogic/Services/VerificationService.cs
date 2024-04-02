using AutoMapper;
using InstrumentationAccountingSystem.BusinessLogic.Interfaces;
using InstrumentationAccountingSystem.Common.Dto;
using InstrumentationAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentationAccountingSystem.BusinessLogic.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public VerificationService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public void Create(VerificationCreateDto verificationCreateDto)
        {
            var verification = _mapper.Map<VerificationCreateDto, Verification>(verificationCreateDto);

            _applicationContext.Verifications.Add(verification);
            _applicationContext.SaveChanges();
        }

        public List<Verification> GetAll()
        {
            var verifications = _applicationContext.Verifications.ToList();

            return verifications;
        }

        public void EditVerification(Verification verification)
        {
            _applicationContext.Verifications.Update(verification);
            _applicationContext.SaveChanges();
        }

        public void DeleteVerificationById(int id) 
        {
            var verification = _applicationContext.Verifications.FirstOrDefault(u => u.Id == id);

            _applicationContext.Verifications.Remove(verification);
            _applicationContext.SaveChanges();
        }

        public Verification GetVerificationById(int id)
        {
            var verification = _applicationContext.Verifications.FirstOrDefault(u => u.Id == id);

            return verification;
        }

        public Verification? GetLastVerificationByInstrumentationId(int id) 
        {
            var verifications = _applicationContext.Verifications.ToList();
            verifications = verifications.OrderByDescending(u => u.Date).ToList();

            return verifications.FirstOrDefault(u => u.InstrumentationId == id);
        }
    }
}
