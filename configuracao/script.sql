CREATE SCHEMA `tarefas` DEFAULT CHARACTER SET utf8;

USE tarefas;

CREATE TABLE IF NOT EXISTS `tarefas`.`tarefa` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `descricao` VARCHAR(200) NOT NULL,
  `concluida` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;

SELECT * FROM tarefa;

INSERT INTO `tarefas`.`tarefa` (`descricao`, `concluida`) VALUES ('Estudar C#', '0');
INSERT INTO `tarefas`.`tarefa` (`descricao`, `concluida`) VALUES ('Estuda MySQL', '0');
INSERT INTO `tarefas`.`tarefa` (`descricao`, `concluida`) VALUES ('Estudar Git', '1');