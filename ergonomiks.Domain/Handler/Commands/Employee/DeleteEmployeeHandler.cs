using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Domain.Commands.Employee;
using ergonomiks.Domain.Interfaces;
using Flunt.Notifications;

namespace ergonomiks.Domain.Handler.Commands.Employee
{
    public class DeleteEmployeeHandler : Notifiable<Notification>, IHandlerCommand<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IUserRepository _userRepository;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository, IEquipmentRepository equipmentRepository, IAlertRepository alertRepository, IUserRepository userRepository)
        {
            _employeeRepository = employeeRepository;
            _equipmentRepository = equipmentRepository;
            _alertRepository = alertRepository;
            _userRepository = userRepository;
        }

        public ICommandResult Handle(DeleteEmployeeCommand command)
        {
            //Verify if employee exists
            var employee = _employeeRepository.GetById(command.Id);
            var emplyeeUser = _userRepository.GetById(employee.IdUser);
            var employeeEquipment = _equipmentRepository.GetByIdEmployee(employee.Id);
            var alertsList = _alertRepository.GetByIdEmployee(employee.Id);

            if (employee != null)
            {
                foreach (var item in alertsList)
                {
                    _alertRepository.Delete(item.Id);
                }

                if (employeeEquipment == null)
                {
                    _employeeRepository.Delete(command.Id);
                    _userRepository.Delete(emplyeeUser.Id);

                    return new GenericCommandResult(true, "Employee deleted", null);
                }

                _equipmentRepository.Delete(employeeEquipment.Id);
                _employeeRepository.Delete(command.Id);
                _userRepository.Delete(emplyeeUser.Id);

                //FOREACH de Alertas

                return new GenericCommandResult(true, "Employee deleted", null);
            }

            return new GenericCommandResult(true, "Employee not found", null);
        }
    }
}
